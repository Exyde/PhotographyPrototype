using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using System;

enum State{
    Photography
}

public class Polaroid : MonoBehaviour
{
    //Might be a static instance ?

    #region Fields
    [Header ("References")] //@TODO : Replace thoses by Static Instance or Singletons ?
    public ObjectManager _objetManager;
    public Dashboard _dashboard;
    [Space(5)]

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
    [SerializeField] public static int _pictureTakensCount = 0;
    ///<summary>
    /// Texture reference to the XNod Picture.
    /// </summary>
    [SerializeField] Picture[] _pictures;
    [SerializeField] Texture2D[] _picturesTextures; //@TODO : Will be generated and/or moved in the Picture Class itself. Will be a reference to the picture class
 
    [Space(10)]

    [Header("Photography Mecanic Settings")]
    [SerializeField] bool _photographyEnabled = true;
    [SerializeField][Tooltip("Nombre max de photos sur soi")] int _maxPicturesSlots = 3;
    [SerializeField][Tooltip("Distance maximale de photographie")][Range(1, 20)] float _photographyMaxDistance = 10f;
    [SerializeField] LayerMask _picturableLayer;
    [SerializeField] ParticleSystem _PS_Flash;

    [Header ("Events")]
    public static Action _OnCabineEnter;
    public static Action _OnCabineExit;

    public static Action<Object_XNod> OnPictureTaken; //Picture, Texture, GameObject => 
    public static Action<Object_XNod> OnSlotSelection; //Picture, Texture, GameObject => 

    #endregion

    #region UnityCallbacks
    void Start(){
        ResetPicturesArrayAndList();
    }

    //@TODO : Put this on Cabine Class
    private void OnEnable() {
        _OnCabineExit += ResetPolaroid;
        _OnCabineExit += CallManagerUpdateList;
    }

    private void OnDisable() {
        _OnCabineExit -= ResetPolaroid;
        _OnCabineExit -= CallManagerUpdateList;            
    }

    void Update()
    {
        if (!_photographyEnabled) return;

        if(_state == State.Photography){
            if((Input.GetKeyDown(GameInputs.PhotographyKeyCode) || Input.GetMouseButtonDown(GameInputs.PhotographyMouseButton))){
                TakePicture();
            } 
            else if (Input.GetKeyDown(GameInputs.PhotographyResetKeyCode)){
                ResetPictureTaken();
            }
        }
    }

    #endregion
    #region Picture Methods
    public void TogglePhotographyMechanic(bool state) => _photographyEnabled = state; //@DESIGN : Make it static ?
    bool CanTakePicture() => _pictureTakensCount < _maxPicturesSlots;
    public void TakePicture(){
        PicturableObject picturable = GetPicturableObject();
        if (picturable == null) return;

        if(!CanTakePicture()) return;

            int slotIndex = GetAvailableSlotIndex();
            Logger.LogInfo("Slot index : " + slotIndex);

            if (slotIndex >= 0){ //If a slot is available

                _currentXnodPicturedObjects[slotIndex] = picturable.GetObject_XNod();

                StartCoroutine(CreatePictureScriptable(slotIndex));

                _pictureTakenSlots[slotIndex] = true;
                
                _pictureTakensCount++;

                OnPictureTaken?.Invoke(picturable.GetObject_XNod());
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

    private int GetAvailableSlotIndex(){
        for (int i = 0; i < _maxPicturesSlots; i++){
            if (_pictureTakenSlots[i] == false) return i;
        }
        return -1;
    }

    private void ResetPictureTaken()
    {
        throw new NotImplementedException();
    }
    
    #endregion
    #region Event Triggers
    private void OnTriggerEnter(Collider other) {
        Logger.LogInfo("Trigger enter with " + other.gameObject.name);
        if (other.gameObject.tag == "Cabine"){
            _OnCabineEnter?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other) {
        Logger.LogInfo("Trigger exit with " + other.gameObject.name);
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

#if false //@TODO : FIX THIS AND EXTRACT
        for (int i =0; i < _maxPicturesSlots; i++){ // Need to check for a sprite ? In the Picture ?
            if (true){ //ULTRA TEMP
                //Todo: CreatePicture On Board
                Dashboard._instance.CreatePictureOnBoard(Sprite.Create(_picturesTextures[i], new Rect(0, 0, 256, 256), Vector2.zero, 32f));

                if (SaveSystem.IsEnabled())
                    SaveSystem.SaveTexToPng(_picturesTextures[i], _picturesTextures[i].name); //@TODO : Temp, remove this elsewhere
            }
        }
#endif
        
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

    }
}

