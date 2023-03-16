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

    public static Action<Dialogue_XNod> OnDialogueFinishRunningEarly;


    //IMPORTANT VARIABLES

    Dialogue_XNod _dialogRuning;

    //USEFUL VARIABLES

    List<Dialogue_XNod> _bufferList = new List<Dialogue_XNod>();

    //CAN BE USED VARIABLES

    IEnumerator _coroutine;
    IEnumerator _curentRunDialogue;

    //INIT FUNCTIONS

    void Awake()
    {
        if (DM == null)
        {
            DM = this;
            //DontDestroyOnLoad(this.gameObject);

        }
        else
        {
            Destroy(this);
        }

        _dg?.InitAllNodes();

    }

    //PUBLIC FUNCTIONS

    public void SendDialogue(int tag)
    {
        SendDialogue(tag, true);
    }

    public void SendDialogueForced(int tag)
    {
        SendDialogue(tag, false);
    }

    public void SendDialogue(Dialogue_XNod dialogue)
    {
        SendDialogue(dialogue, true);
    }

    private void SendDialogue(int tag, bool verificationIfAlreadyRun)
    {
        if(tag == 0)
        {
            return;
        }

        Dialogue_XNod dialogue = _dg.GetDialogueWithTag(tag);

        if(dialogue == null)
        {
            Logger.LogInfo("Le Dialogue qui a été lancé n'existe pas. Tag :" + tag);
            return;
        }

        SendDialogue(dialogue, verificationIfAlreadyRun);
    }

    public void SendDialogue(Dialogue_XNod dialogue, bool verificationIfAlreadyRun)
    {
        if (verificationIfAlreadyRun && dialogue.HasBeenRun)
        {
            return;
        }

        if(_dialogRuning !=  null && 
            dialogue.CanInterupt && 
            dialogue.GetPriority() > _dialogRuning.GetPriority())
        {
            RunDialogueWithInteruption(dialogue);
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
        if (_dialogRuning != null || _bufferList.Count == 0)
        {
            return;
        }

        Dialogue_XNod dialogueToRun = GetHighestPriorityDialogInList(_bufferList);

        while (_bufferList.Remove(dialogueToRun));

        _curentRunDialogue = IE_runDialogue(dialogueToRun);
        StartCoroutine(_curentRunDialogue);
    }

    private void tryToPrepareRunDialogue(Dialogue_XNod dialogueToCheckWith)
    {
        _bufferList.Add(dialogueToCheckWith);
        tryToPrepareRunDialogue();
        _bufferList.Remove(dialogueToCheckWith);

    }

    private void RunDialogueWithInteruption(Dialogue_XNod dialogueToRun)
    {
        OnDialogueFinishRunningEarly?.Invoke(_dialogRuning);
        StopCoroutine(_curentRunDialogue);

        _curentRunDialogue = IE_runDialogue(dialogueToRun);
        StartCoroutine(_curentRunDialogue);
    }

    private IEnumerator IE_runDialogue(Dialogue_XNod dialogueToRun)
    {
            _dialogRuning = dialogueToRun;

        yield return new WaitForSeconds(dialogueToRun.PreDialogueTime);

            OnDialogueStartRunning?.Invoke(dialogueToRun);
            
        yield return new WaitForSeconds(dialogueToRun.GetDialogueTime() );

            dialogueToRun.HasBeenRun = true;
                
            OnDialogueFinishRunning?.Invoke(dialogueToRun);

        yield return new WaitForSeconds(dialogueToRun.PostDialogueTime);

            _dialogRuning = null;

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
