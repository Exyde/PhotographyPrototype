using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
using Core.GameEvents;


public class EventDialogueManager : MonoBehaviour
{
    [SerializeField] List<TagToEvent> ActionAtStartOfDialogue = new();

    [SerializeField] List<TagToEvent> ActionAtFinishOfDialogue = new();

    private void OnEnable()
    {
        DialogueManager.OnDialogueStartRunning += OnDialogueStartRuning_EventDialogueManager;
        DialogueManager.OnDialogueFinishRunning += OnDialogueFinishRuning_EventDialogueManager;
    }

    private void OnDisable()
    {
        DialogueManager.OnDialogueStartRunning -= OnDialogueStartRuning_EventDialogueManager;
        DialogueManager.OnDialogueFinishRunning -= OnDialogueFinishRuning_EventDialogueManager;
    }

    private void OnDialogueStartRuning_EventDialogueManager(Dialogue_XNod dialogue)
    {
        foreach (TagToEvent curentTagToEvent in ActionAtStartOfDialogue)
        {
            if(dialogue.Tag == curentTagToEvent.tag)
            {
                curentTagToEvent.ActionAtTag?.Invoke();
            }
        }
    }

    private void OnDialogueFinishRuning_EventDialogueManager(Dialogue_XNod dialogue)
    {
        foreach(FactOperation fo in dialogue.OperationsAfterRun)
        {
            BlackboardManager.BBM?.SetFactValue(fo);
        }
        
        foreach (TagToEvent curentTagToEvent in ActionAtFinishOfDialogue)
        {
            if (dialogue.Tag == curentTagToEvent.tag)
            {
                curentTagToEvent.ActionAtTag?.Invoke();
            }
        }
    }

    [System.Serializable]
    class TagToEvent : ISerializationCallbackReceiver
    {
        [HideInInspector]
        public string name;
        public int tag;
        public UnityEvent ActionAtTag;

        public void OnBeforeSerialize()
        {
            name = "Action at dialogue " + tag.ToString().PadLeft(6, '0');
        }

        public void OnAfterDeserialize()
        {
            name = "Action at dialogue " + tag.ToString().PadLeft(6, '0');
        }
    }

}
