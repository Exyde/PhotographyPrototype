using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//@Rubens : J'ai enleve les accents qui me spammait derreurs 
public class HUDManager : MonoBehaviour
{
    public static HUDManager _instance;
    public TextMeshProUGUI Subtitle;
    public TextMeshProUGUI CurentTouchAvailable;
    public TextMeshProUGUI CountPicturesLeft;

    public TextMeshProUGUI NameOfPicturableObjectOnPointer;

    public GameObject CrossAir;
    public GameObject AllHUD;

    bool TruePoeticFalseTension = true; //A supprimer quand on aura un gamemanager qui nous dit � quel moment du jeu on est

    private void Awake() {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

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

        Cabine._OnCabineEnter += DisplayTensionHUD;
        Cabine._OnCabineExit += DisplayPoeticHUD;

        PicturableObject.OnPointerEnterOnPicturableObject += DisplayNameOfPicturableObject;

    }

    private void OnDisable()
    {
        DialogueManager.OnDialogueStartRunning -= DisplaySubtitle;
        DialogueManager.OnDialogueFinishRunningEarly -= StopDisplaySubtitle;
        DialogueManager.OnDialogueFinishRunning -= StopDisplaySubtitle;

        Polaroid.OnPictureTaken -= OnPictureTaken_HUDManager;
        Polaroid.OnPolaroidReset -= ActualiseHUD;

        Cabine._OnCabineEnter -= DisplayTensionHUD;
        Cabine._OnCabineExit -= DisplayPoeticHUD;

        PicturableObject.OnPointerEnterOnPicturableObject -= DisplayNameOfPicturableObject;
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

    void DisplayNameOfPicturableObject(Object_XNod objectCabine)
    {
        NameOfPicturableObjectOnPointer.text = objectCabine.NameOfTheObject;
    }

    public void StopDisplayNameOfPicturableObject()
    {
        NameOfPicturableObjectOnPointer.text = "";
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

    public void SetActiveHUD(bool b)
    {
        AllHUD.SetActive(b);
    }

    public void SetActivCrossAir(bool b)
    {
        CrossAir.SetActive(b);
    }



}
