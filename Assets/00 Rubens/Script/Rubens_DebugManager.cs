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

    private IEnumerator Start()
    {
        DialogueManager.DM.SendDialogue(0000001);

        yield return new WaitForSeconds(15);

        DialogueManager.DM.SendDialogue(0000003);

    }

    private void SendMessageDebug(Dialogue_XNod dialogue)
    {
        Debug.Log(dialogue.Dialogue);
    }

    
}
