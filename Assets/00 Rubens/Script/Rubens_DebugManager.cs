using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rubens_DebugManager : MonoBehaviour
{
    private void OnEnable()
    {
        DialogueManager.OnDialogueStartRunning += OnDialogueStartRuning_DebugManager;
    }

    private void OnDisable()
    {
        DialogueManager.OnDialogueStartRunning -= OnDialogueStartRuning_DebugManager;
    }

    private void OnDialogueStartRuning_DebugManager(Dialogue_XNod dialogue)
    {
        Debug.Log(dialogue.Dialogue);
    }


    private IEnumerator Start()
    {
        DialogueManager.DM.SendDialogue(1);

        yield return new WaitForSeconds(10);

        DialogueManager.DM.SendDialogue(5);
    }
}
