using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using System;

enum State{
    Photography, SlotSelection
}

public class Polaroid : MonoBehaviour
{
    #region Fields
    [Header ("References")] //@TODO : Replace thoses by Static Instance or Singletons ?
    public ObjectManager _objetManager;
    public Dashboard _dashboard;
    [Space(5)]

    [Header("UI References")] //@TODO : Replace with Event Broadcast & Observer Pattern Within the HUD ?
    public GameObject _UIImagesHolder;
    public Image[] _UIImagePictureSlots;
    public TMP_Text _pictureOverrideTxt;
    [Space(10)]

    [Header ("Preview & Readonly")]
    [SerializeField] State _state = State.Photography;
    ///<summary>
    /// XNod Objects currently saved in pictures slots.
    /// </summary>
    [SerializeField] Object_XNod[] _currentXnodPicturedObjects;
   
    ///<summary>
    /// XNod Objects selected for tag update in the XNod Graph.
    /// </summary>
    [SerializeField] List<Object_XNod> _xNodSelectedPicturablesObjets = new List<Object_XNod>();

    [Space (5)]
    ///<summary>
    /// Currently Available Slots. (false is empty, true is taken). @TODO : "Inventory Class"? 
    /// </summary>
    [SerializeField] bool[] _pictureTakenSlots;
    [SerializeField] int _pictureTakensCount = 0;
    ///<summary>
    /// Texture reference to the XNod Picture.
    /// </summary>
    [SerializeField] Picture[] _pictures;
    [SerializeField] Texture2D[] _picturesTextures; //@TODO : Will be generated and/or moved in the Picture Class itself. Will be a reference to the picture class
 
    [Space(10)]

    [Header("Photography Mecanic Settings")]
    [SerializeField] bool _photographyEnabled = true;
    [SerializeField][Tooltip("Nombre max de photos sur soi")] int _maxPicturesSlots = 3;
    [SerializeField][Tooltip("Prise de photo automatique après avoir détruit une photo ?")] bool _automaticPictureTakenAfterSlotSelection = false;
    [SerializeField][Tooltip("Distance maximale de photographie")][Range(1, 20)] float _photographyMaxDistance = 10f;
    [SerializeField] LayerMask _picturableLayer;
    [SerializeField] ParticleSystem _PS_Flash;

    [Header ("Events")]
    public Action _OnCabineEnter;
    public Action _OnCabineExit;

    public static Action<Object_XNod> OnPictureTaken; //Picture, Texture, GameObject => 
    public static Action<Object_XNod> OnSlotSelection; //Picture, Texture, GameObject => 

    #endregion

    #region UnityCallbacks
    void Start(){
        ResetPicturesArrayAndList();
        if (_UIImagesHolder) _UIImagesHolder.SetActive(false); //@TODO : REMOVE THIS é_è

        _OnCabineExit += ResetPolaroid; //This aswell
        _OnCabineExit += CallManagerUpdateList; //and this
    }

    void Update()
    {
        if (!_photographyEnabled) return;

        if(_state == State.Photography){
            if((Input.GetKeyDown(KeyCode.P) || Input.GetMouseButtonDown(0))){
                TakePicture();
            }
        }

        if (_state == State.SlotSelection) HandlePictureSlotSelection();
    }
    #endregion
    #region Picture Methods

    public void TogglePhotographyMechanic(bool state) => _photographyEnabled = state; //@DESIGN : Make it static ?
    public void TakePicture(){

        PicturableObject picturable = GetPicturableObject();
        if (picturable == null) return;

        if(CanTakePicture()){
            int slotIndex = GetAvailableSlotIndex();
            Logger.LogInfo("Slot index : " + slotIndex);

            if (slotIndex >= 0){ //If a slot is available

                StartCoroutine(CreatePictureScriptable(slotIndex));
                _currentXnodPicturedObjects[slotIndex] = picturable.GetObject_XNod();
                _pictureTakenSlots[slotIndex] = true;
                OnPictureTaken?.Invoke(picturable.GetObject_XNod());

                _pictureTakensCount++;
            }
        }
        else {
            _state = State.SlotSelection;
        }
    }

    PicturableObject GetPicturableObject(){
        RaycastHit hit;
        PicturableObject picturable = null;
        if (Physics.Raycast(transform.position, transform.forward, out hit, _photographyMaxDistance, _picturableLayer)){
            picturable = hit.collider.gameObject.GetComponent<PicturableObject>();
        }

        return picturable;
    }

    bool CanTakePicture() => _pictureTakensCount < _maxPicturesSlots;

    private void HandlePictureSlotSelection(){
        _UIImagesHolder.SetActive(true);
        _pictureOverrideTxt.gameObject.SetActive(true);
        int playerPictureSlotIndexSelected = -1;
        
        if (Input.GetKeyDown(KeyCode.I)) playerPictureSlotIndexSelected = 0;
        else if (Input.GetKeyDown(KeyCode.O)) playerPictureSlotIndexSelected = 1;
        else if (Input.GetKeyDown(KeyCode.P)) playerPictureSlotIndexSelected = 2;
        else if (Input.GetKey(KeyCode.Q)){
            _pictureOverrideTxt.gameObject.SetActive(false);
            _state = State.Photography;
            return;
        }

        if (playerPictureSlotIndexSelected == -1) return;

        _pictureTakenSlots[playerPictureSlotIndexSelected] = false;
        _currentXnodPicturedObjects[playerPictureSlotIndexSelected] = null;
        _picturesTextures[playerPictureSlotIndexSelected] = null;
        _UIImagePictureSlots[playerPictureSlotIndexSelected].sprite = null;
        _pictureTakensCount--;

         _state = State.Photography;

        _pictureOverrideTxt.gameObject.SetActive(false);
        _UIImagesHolder.SetActive(false);


        if (_automaticPictureTakenAfterSlotSelection) TakePicture();
    }

    private int GetAvailableSlotIndex(){
        for (int i = 0; i < _maxPicturesSlots; i++){
            if (_pictureTakenSlots[i] == false) return i;
        }
        return -1;
    }
    
    #endregion
    #region Event Triggers
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Cabine"){
            _OnCabineEnter?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Cabine"){
            _OnCabineExit?.Invoke();
        } 
    }
    
    void CallManagerUpdateList(){
        _objetManager.UpdateObjectAndSpawnObjectInCabine(_objetManager._objectsInCabineCount);
    }
    #endregion
    #region Resets
    void ResetPolaroid(){
        Debug.Log("Reset Polaroid.");
        for (int i = 0; i < _currentXnodPicturedObjects.Length; i++){
            _xNodSelectedPicturablesObjets.Add(_currentXnodPicturedObjects[i]);
        }

        for (int i =0; i < _maxPicturesSlots; i++){
            if (_UIImagePictureSlots[i].sprite != null){
                _dashboard.CreatePictureOnBoard(_UIImagePictureSlots[i].sprite);
                SaveSystem.SaveTexToPng(_picturesTextures[i], _picturesTextures[i].name); //@TODO : Temp, remove this elsewhere
                _UIImagePictureSlots[i].sprite = null;
            }
        }
        
        _objetManager.UpdatePicturedXNodeObjets(_xNodSelectedPicturablesObjets);

        ResetPicturesArrayAndList();
    }

    void ResetPicturesArrayAndList(){
        //Logger.Log("Reseting Arrays");
        _xNodSelectedPicturablesObjets.Clear();
        _currentXnodPicturedObjects = new Object_XNod[_maxPicturesSlots];
        _picturesTextures = new Texture2D[_maxPicturesSlots];
        _pictures = new Picture[_maxPicturesSlots];

        _pictureTakenSlots = new bool[_maxPicturesSlots];
        for (int i =0; i < _maxPicturesSlots; i++){
            _pictureTakenSlots[i] = false;
        }
        _pictureTakensCount = 0;
        // Logger.Log("Finish Reseting Arrays");
    }

    #endregion

    void OnDrawGizmos(){
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * _photographyMaxDistance);
    }

    IEnumerator CreatePictureScriptable(int index){
        Picture picture = ScriptableObject.CreateInstance<Picture>(); // @TODO : Save and Load this in database ?

        picture.name = _currentXnodPicturedObjects[index].name;

        StartCoroutine(picture.CreateTextureAndSprite());
        _pictures[index] = picture;

        yield return new WaitForEndOfFrame();

        _picturesTextures[index] = picture.GetTexture();
        _UIImagePictureSlots[index].sprite = picture.GetSprite();

    }
}

