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

    public static Action<Dialogue_XNod> OnDialogueFinishRunning;

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
        SendDialogue(tag, true);
    }

    public void SendDialogue(Dialogue_XNod dialogue)
    {
        SendDialogue(dialogue, true);
    }

    public void SendDialogue(int tag, bool verificationIfAlreadyRun)
    {
        Dialogue_XNod dialogue = _dg.GetDialogueWithTag(tag);

        if (verificationIfAlreadyRun && dialogue.HasBeenRun)
        {
            return;
        }

        _bufferList.Add(dialogue);

        _coroutine = IE_cleanerBuffer(dialogue);

        StartCoroutine(_coroutine);

        tryToPrepareRunDialogue();
    }

    public void SendDialogue(Dialogue_XNod dialogue, bool verificationIfAlreadyRun)
    {
        if (verificationIfAlreadyRun && dialogue.HasBeenRun)
        {
            return;
        }

        _bufferList.Add(dialogue);

        _coroutine = IE_cleanerBuffer(dialogue);

        StartCoroutine(_coroutine);

        tryToPrepareRunDialogue();
    }

    //PRIVATES FUNCTIONS

    private void tryToPrepareRunDialogue()
    {
        if (_isADialogRuning == true || _bufferList.Count == 0)
        {
            return;
        }

        Dialogue_XNod dialogueToRun = GetHighestPriorityDialogInList(_bufferList);

        while (_bufferList.Remove(dialogueToRun));

        _coroutine = IE_runDialogue(dialogueToRun);
        StartCoroutine(_coroutine);

        if (!dialogueToRun.HaveNextDialogue())
        {
            return;
        }
        /*
        Dialogue_XNod nextDialog = dialogueToRun.GetNextDialogue();

        SendDialogue(nextDialog, false);*/
    }

    private void tryToPrepareRunDialogue(Dialogue_XNod dialogueToCheckWith)
    {
        _bufferList.Add(dialogueToCheckWith);
        tryToPrepareRunDialogue();
        _bufferList.Remove(dialogueToCheckWith);

    }

    private IEnumerator IE_runDialogue(Dialogue_XNod dialogueToRun)
    {
            _isADialogRuning = true;

        yield return new WaitForSeconds(dialogueToRun.PreDialogueTime);

            dialogueToRun.HasBeenRun = true;

            OnDialogueStartRunning?.Invoke(dialogueToRun);
            
        yield return new WaitForSeconds(dialogueToRun.GetDialogueTime() );
        
            OnDialogueFinishRunning?.Invoke(dialogueToRun);

        yield return new WaitForSeconds(dialogueToRun.PostDialogueTime);

            _isADialogRuning = false;

        if (dialogueToRun.HaveNextDialogue())
        {
            tryToPrepareRunDialogue(dialogueToRun.GetNextDialogue() );
        }
        else
        {
            tryToPrepareRunDialogue();
        }

    }

    private IEnumerator IE_cleanerBuffer(Dialogue_XNod dialog)
    {
        yield return new WaitForSeconds( dialog.GetBufferTime() );

        _bufferList.Remove(dialog);
    }




    //PRIVATES TOOL FUNCTIONS

    private Dialogue_XNod GetHighestPriorityDialogInList(List<Dialogue_XNod> listDialog)
    {
        if (_bufferList.Count == 0)
        {
            return null;
        }

        Dialogue_XNod dialogueToSend = null;
            
        int highestPriority = 0;

        foreach(Dialogue_XNod dialogueTested in listDialog)
        {
            int testedPriority = dialogueTested.GetPriority();

            if (testedPriority >= highestPriority)
            {
                highestPriority = testedPriority;

                dialogueToSend = dialogueTested;
            }
        }

        return dialogueToSend;
    }

    

}
