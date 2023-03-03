using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    //VERY IMPORTANT

    DialogueToolGraph_XNod _dg;

    public static DialogManager DM;

    //IMPORTANT

    bool IsADialogRuning;

    //USEFUL

    List<Dialogue_XNod> _bufferList = new List<Dialogue_XNod>();

    //CAN BE USED

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
    }

    //PUBLIC FUNCTIONS

    public void SendDialogue(int tag)
    {
        Dialogue_XNod dialogue = _dg.GetDialogueWithTag(tag);

        _bufferList.Add(dialogue);

        _coroutine = IE_CleanBuffer(dialogue);

        StartCoroutine(_coroutine);

        tryToPrepareRunDialogue();
    }
    
    private void tryToPrepareRunDialogue()
    {
        if (IsADialogRuning == true)
        {
            return;
        }

        if (_bufferList.Count == 0)
        {
            return;
        }

        Dialogue_XNod dialogueToRun = GetHighestPriorityDialogInList(_bufferList);

        _bufferList.Remove(dialogueToRun);

        //Faire le Run Dialogue mtnt (pour mettre le dialogue encours à true et pas jouer le dialogue suivant qui a une priorité plus haute)

        //Verifier si il y a un dialogue suivant 
        
        //Get le dialogue suivant

        //SendDialog du dialogue suivant.

    }

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

    private IEnumerator IE_CleanBuffer(Dialogue_XNod dialog)
    {
        yield return new WaitForSeconds(dialog.BufferTime);

        _bufferList.Remove(dialog);
    }

}
