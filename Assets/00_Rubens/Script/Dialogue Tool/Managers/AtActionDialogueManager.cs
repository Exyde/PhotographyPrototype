using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtActionDialogueManager : MonoBehaviour
{

    private void OnEnable()
    {
        Polaroid.OnPictureTaken += SendDialogueAtPicture;
    }

    private void OnDisable()
    {
        Polaroid.OnPictureTaken -= SendDialogueAtPicture;
    }


    void SendDialogueAtPicture(Object_XNod objectPictured)
    {
        DialogueManager.DM?.SendDialogue(objectPictured.DialogueAtPicture);
    }

    void SendDialogueAtDashBoard(Object_XNod objectDashboard)
    {
        DialogueManager.DM?.SendDialogue(objectDashboard.DialogueAtLookDashboard);
    }

}
