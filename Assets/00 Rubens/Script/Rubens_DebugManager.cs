using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rubens_DebugManager : MonoBehaviour
{
    private void OnEnable()
    {
        DialogueManager.OnDialogueStartRunning += SendMessageDebug;
    }

    private void OnDisable()
    {
        DialogueManager.OnDialogueStartRunning -= SendMessageDebug;
    }

    private void SendMessageDebug(Dialogue_XNod dialogue)
    {
        Debug.Log(dialogue.Dialogue);
    }


    private IEnumerator Start()
    {
        DialogueManager.DM.SendDialogue(1);

        yield return new WaitForSeconds(12);

        DialogueManager.DM.SendDialogue(3);
    }

    

    
}
