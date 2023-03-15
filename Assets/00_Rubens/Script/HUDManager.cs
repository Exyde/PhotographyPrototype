using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
    public TextMeshProUGUI Subtitle;
    public TextMeshProUGUI CurentTouchAvailable;
    public TextMeshProUGUI CountPicturesLeft;

    bool TruePoeticFalseTension = true; //A supprimer quand on aura un gamemanager qui nous dit à quel moment du jeu on est

    private void Start()
    {
        ActualiseHUD();
    }

    private void OnEnable()
    {
        DialogueManager.OnDialogueStartRunning += DisplaySubtitle;
        DialogueManager.OnDialogueFinishRunningEarly += StopDisplaySubtitle;
        DialogueManager.OnDialogueFinishRunning += StopDisplaySubtitle;

        Polaroid.OnPictureTaken += OnPictureTaken_HUDManager;

        Polaroid._OnCabineEnter += DisplayTensionHUD;
        Polaroid._OnCabineExit += DisplayPoeticHUD;
    }

    private void OnDisable()
    {
        DialogueManager.OnDialogueStartRunning -= DisplaySubtitle;
        DialogueManager.OnDialogueFinishRunningEarly -= StopDisplaySubtitle;
        DialogueManager.OnDialogueFinishRunning -= StopDisplaySubtitle;

        Polaroid.OnPictureTaken -= OnPictureTaken_HUDManager;

        Polaroid._OnCabineEnter -= DisplayTensionHUD;
        Polaroid._OnCabineExit -= DisplayPoeticHUD;
    }

    void DisplaySubtitle(Dialogue_XNod dialogue)
    {
        Subtitle.text = dialogue.Dialogue;
    }

    void StopDisplaySubtitle(Dialogue_XNod dialogue)
    {
        Subtitle.text = "";
    }

    void DisplayTensionHUD()
    {
        TruePoeticFalseTension = false;
        ActualiseHUD();
    }

    void DisplayPoeticHUD()
    {
        TruePoeticFalseTension = true;
        ActualiseHUD();
    }


    void OnPictureTaken_HUDManager(Object_XNod objectCabine)
    {
        ActualiseHUD();
    }

    void ActualiseHUD()
    {
        ActualiseTouchAvailableHUD();
        ActualiseCountPhotography();
    }

    void ActualiseTouchAvailableHUD()
    {
        CurentTouchAvailable.text = "Appuyez sur ZQSD pour se déplacer.\n";

        if (!TruePoeticFalseTension)
        {
            CurentTouchAvailable.text += "Appuie sur le clic gauche pour photographier.\n";

            if (Polaroid._pictureTakensCount > 0)
            {
                CurentTouchAvailable.text += "Appuie sur R pour réamorcer la pellicule.\n";
            }

        }
    }

    void ActualiseCountPhotography()
    {
        if (TruePoeticFalseTension)
        {
            CountPicturesLeft.text = "";
            return;
        }

        int PhotoLeft = Mathf.Abs( Polaroid._pictureTakensCount - 3);

        CountPicturesLeft.text = "Nombre de photos réstantes : " + PhotoLeft;

        if(PhotoLeft == 0)
        {
            CountPicturesLeft.color = Color.red;
        }
        else
        {
            CountPicturesLeft.color = Color.white;
        }
    }



}
