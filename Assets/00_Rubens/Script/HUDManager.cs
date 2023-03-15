using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//@Rubens : J'ai enleve les accents qui me spammait derreurs 
public class HUDManager : MonoBehaviour
{
    public TextMeshProUGUI Subtitle;
    public TextMeshProUGUI CurentTouchAvailable;
    public TextMeshProUGUI CountPicturesLeft;

    bool TruePoeticFalseTension = true; //A supprimer quand on aura un gamemanager qui nous dit � quel moment du jeu on est

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
        Polaroid.OnPolaroidReset += ActualiseHUD;

        Polaroid._OnCabineEnter += DisplayTensionHUD;
        Polaroid._OnCabineExit += DisplayPoeticHUD;
    }

    private void OnDisable()
    {
        DialogueManager.OnDialogueStartRunning -= DisplaySubtitle;
        DialogueManager.OnDialogueFinishRunningEarly -= StopDisplaySubtitle;
        DialogueManager.OnDialogueFinishRunning -= StopDisplaySubtitle;

        Polaroid.OnPictureTaken -= OnPictureTaken_HUDManager;
        Polaroid.OnPolaroidReset -= ActualiseHUD;

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
        CurentTouchAvailable.text = "Appuyez sur ZQSD pour se deplacer.\n";

        if (!TruePoeticFalseTension)
        {
            CurentTouchAvailable.text += "Appuie sur le clic gauche pour photographier.\n";

            if (Polaroid._pictureTakensCount > 0)
            {
                CurentTouchAvailable.text += "Appuie sur R pour reamorcer la pellicule.\n";
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

        CountPicturesLeft.text = "Nombre de photos restantes : " + PhotoLeft;

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
