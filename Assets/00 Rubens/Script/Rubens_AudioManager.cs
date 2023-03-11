using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rubens_AudioManager : MonoBehaviour
{

    public Transform Player;

    private void OnEnable()
    {
        DialogueManager.OnDialogueStartRunning += OnDialogueStartRuning_AudioManager;
    }

    private void OnDisable()
    {
        DialogueManager.OnDialogueStartRunning -= OnDialogueStartRuning_AudioManager;
    }

    private void OnDialogueStartRuning_AudioManager(Dialogue_XNod dialogue)
    {
       if(dialogue.AudioClipDialogue != null)
        {
            AudioSource.PlayClipAtPoint(dialogue.AudioClipDialogue, Player.position);

        }
    }
}
