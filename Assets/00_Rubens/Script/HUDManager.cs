using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
    public TextMeshProUGUI Subtitle;
    
    private void OnEnable()
    {
        DialogueManager.OnDialogueStartRunning += DisplaySubtitle;
        DialogueManager.OnDialogueFinishRunningEarly += StopDisplaySubtitle;
        DialogueManager.OnDialogueFinishRunning += StopDisplaySubtitle;

        Polaroid.OnPictureTaken += DisplayPictureInHUD;
    }

    private void OnDisable()
    {
        DialogueManager.OnDialogueStartRunning -= DisplaySubtitle;
        DialogueManager.OnDialogueFinishRunningEarly -= StopDisplaySubtitle;
        DialogueManager.OnDialogueFinishRunning -= StopDisplaySubtitle;

        Polaroid.OnPictureTaken -= DisplayPictureInHUD;
    }

    void DisplaySubtitle(Dialogue_XNod dialogue)
    {
        Subtitle.text = dialogue.Dialogue;
    }

    void StopDisplaySubtitle(Dialogue_XNod dialogue)
    {
        Subtitle.text = "";
    }

    void DisplayPictureInHUD(Object_XNod objectCabine)
    {

    }



}
