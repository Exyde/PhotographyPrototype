using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtActionDialogueManager : MonoBehaviour
{
    public List<int> DialogueRandomOnEnterCabine = new();

    private void Start()
    {
        DialogueManager.DM?.SendDialogue(1);
    }
    private void OnEnable()
    {
        Polaroid.OnPictureTaken += SendDialogueAtPicture;

        Cabine._OnCabineEnter += SendDialogueAtEnterCabine;
        Cabine._OnCabineExit += SendDialogueAtExitCabine;

    }

    private void OnDisable()
    {
        Polaroid.OnPictureTaken -= SendDialogueAtPicture;

        Cabine._OnCabineEnter += SendDialogueAtEnterCabine;
        Cabine._OnCabineExit += SendDialogueAtExitCabine;
    }


    void SendDialogueAtPicture(Object_XNod objectPictured)
    {
        DialogueManager.DM?.SendDialogue(objectPictured.DialogueAtPicture);
    }

    void SendDialogueAtDashBoard(Object_XNod objectDashboard)
    {
        DialogueManager.DM?.SendDialogue(objectDashboard.DialogueAtLookDashboard);
    }

    void SendDialogueAtEnterCabine()
    {

    }

    void SendDialogueAtExitCabine()
    {
        DialogueManager.DM?.SendDialogue(DialogueRandomOnEnterCabine[Random.Range(0, DialogueRandomOnEnterCabine.Count-1) ]);
    }

}
