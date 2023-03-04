using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    //VERY IMPORTANT VARIABLES

    public static DialogueManager DM;
    
    public DialogueToolGraph_XNod _dg;

    public static Action<Dialogue_XNod> OnDialogueStartRunning;

    public static Action OnDialogueFinishRunning;

    //IMPORTANT VARIABLES

    bool _isADialogRuning;

    //USEFUL VARIABLES

    List<Dialogue_XNod> _bufferList = new List<Dialogue_XNod>();

    //CAN BE USED VARIABLES

    IEnumerator _coroutine;

    //INIT FUNCTIONS

    void Awake()
    {
        if (DM == null)
        {
            DM = this;
            DontDestroyOnLoad(this.gameObject);

        }
        else
        {
            Destroy(this);
        }

        _dg.InitAllNodes();

    }

    //PUBLIC FUNCTIONS

    public void SendDialogue(int tag)
    {
        Dialogue_XNod dialogue = _dg.GetDialogueWithTag(tag);

        _bufferList.Add(dialogue);

        _coroutine = IE_CleanerBuffer(dialogue);

        StartCoroutine(_coroutine);

        tryToPrepareRunDialogue();
    }

    public void SendDialogue(Dialogue_XNod dialogue)
    {
        _bufferList.Add(dialogue);

        _coroutine = IE_CleanerBuffer(dialogue);

        StartCoroutine(_coroutine);

        tryToPrepareRunDialogue();
    }

    //PRIVATES FUNCTIONS

    private void tryToPrepareRunDialogue()
    {
        if (_isADialogRuning == true)
        {
            return;
        }

        if (_bufferList.Count == 0)
        {
            return;
        }

        Dialogue_XNod dialogueToRun = GetHighestPriorityDialogInList(_bufferList);

        _bufferList.Remove(dialogueToRun);

        _coroutine = runDialogue(dialogueToRun);
        StartCoroutine(_coroutine);

        if (!dialogueToRun.GetOutputPort("NextDialogue").IsConnected)
        {
            return;
        }

        Dialogue_XNod nextDialog = dialogueToRun.GetOutputPort("NextDialogue").Connection.node as Dialogue_XNod;

        SendDialogue(nextDialog);
    }

    private IEnumerator runDialogue(Dialogue_XNod dialogueToRun)
    {
            _isADialogRuning = true;

        yield return new WaitForSeconds(dialogueToRun.PreDialogueTime);

            dialogueToRun.HasBeenPlayed = true;

            OnDialogueStartRunning?.Invoke(dialogueToRun);
            
            if (dialogueToRun.AudioClipDialogue == null )
            {
                yield return new WaitForSeconds(dialogueToRun.DefaultTime);
            }
            else
            {
                yield return new WaitForSeconds(dialogueToRun.AudioClipDialogue.length);
            }

        
        OnDialogueFinishRunning?.Invoke();

        yield return new WaitForSeconds(dialogueToRun.PostDialogueTime);

            _isADialogRuning = false;

            tryToPrepareRunDialogue();

    }

    private IEnumerator IE_CleanerBuffer(Dialogue_XNod dialog)
    {
        yield return new WaitForSeconds(dialog.BufferTime);

        _bufferList.Remove(dialog);
    }


    //PRIVATES TOOL FUNCTIONS

    private Dialogue_XNod GetHighestPriorityDialogInList(List<Dialogue_XNod> listDialog)
    {
        Dialogue_XNod dialogueToSend = null;
            
        int highestPriority = -1;

        foreach(Dialogue_XNod dialogueTested in listDialog)
        {
            int testedPriority = dialogueTested.Priority;

            if (testedPriority >= highestPriority)
            {
                highestPriority = testedPriority;

                dialogueToSend = dialogueTested;
            }
        }

        return dialogueToSend;
    }

    

}
