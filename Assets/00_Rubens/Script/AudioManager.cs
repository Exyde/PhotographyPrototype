using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{

    public Sound[] Sounds;

    AudioSource DialogueSource;
    AudioSource InterupetedDialogueSource;

    private void OnEnable()
    {
        DialogueManager.OnDialogueStartRunning += PlayAudioDialogue;
        DialogueManager.OnDialogueFinishRunningEarly += InteruptAudioDialogue;
    }

    private void OnDisable()
    {
        DialogueManager.OnDialogueStartRunning -= PlayAudioDialogue;
        DialogueManager.OnDialogueFinishRunningEarly -= InteruptAudioDialogue;

    }

    private void Awake()
    {
        DialogueSource = gameObject.AddComponent<AudioSource>();
        InterupetedDialogueSource = gameObject.AddComponent<AudioSource>();


        foreach (Sound sound in Sounds)
        {
            sound.Source = gameObject.AddComponent<AudioSource>();

            sound.Source.clip = sound.Clip;
            sound.Source.volume = sound.Volume;
            sound.Source.pitch = sound.Pitch;
        }
    }

    public void Play(string SoundName)
    {
        Sound s = Array.Find(Sounds, Sound => Sound.Name == SoundName);

        if(s == null)
        {
            Debug.LogWarning("Le son " + SoundName + "n'existe pas mais essaie d'�tre jou�.");
            return;
        }

        s.Source.Play();
    }

    private void PlayAudioDialogue(Dialogue_XNod dialogue)
    {
        if (dialogue.AudioClipDialogue != null)
        {
            DialogueSource.clip = dialogue.AudioClipDialogue;
            DialogueSource.Play();
        }
    }

    private void InteruptAudioDialogue(Dialogue_XNod dialogue)
    {
        DialogueSource.Stop();
    }
}

[System.Serializable]
public class Sound
{
    public string Name;

    public AudioClip Clip;

    public AudioSource Source;

    [Range(0f,1f)]
    public float Volume;

    [Range(0.1f, 3f)]
    public float Pitch;

    public bool Loop;
}
