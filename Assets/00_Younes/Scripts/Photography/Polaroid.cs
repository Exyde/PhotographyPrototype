using System.Collections;
using UnityEngine;
using System;

    //Design
    //Might be a static instance ?
    //Pas prnedre en photo un objet déjà pris ?

    //Todo :
    // Remove Object Manger ref => Event

    // Save the picture asset when created for later Game ? [Out of scope]
   
enum State{ Photography }

public class Polaroid : MonoBehaviour
{
    #region Fields
    [Header ("References")] //@TODO : Replace thoses by Static Instance or Singletons ?
    public ObjectManager _objetManager;
    [Header ("Preview & Readonly")]
    [SerializeField] State _state = State.Photography;
    [SerializeField] Object_XNod[] _currentXnodPicturedObjects;

    [Space (5)]
    [SerializeField] public static int _pictureTakensCount = 0;
    [SerializeField] Picture[] _pictures;
    [SerializeField] bool[] _pictureTakenSlots;
 
    [Space(10)]

    [Header("Photography Mecanic Settings")]
    [SerializeField] bool _photographyEnabled = true;
    [SerializeField][Tooltip("Nombre max de photos sur soi")] int _maxPicturesSlots = 3;
    [SerializeField][Tooltip("Distance maximale de photographie")][Range(1, 20)] float _photographyMaxDistance = 10f;
    [SerializeField] LayerMask _picturableLayer;

    [Header ("Events")]

    public static Action<Object_XNod> OnPictureTaken;
    public static Action OnPolaroidReset;

    #endregion

    #region UnityCallbacks
    void Start(){
        ResetPolaroid();
    }

    //@TODO : Put this on Cabine Class
    private void OnEnable() {
        StoryManager.EndOfDay += UpdateXNodObjectPictureTakenTag;
        StoryManager.EndOfDay += CallObjectManagerUpdateListAndSpawnObject;
        StoryManager.EndOfDay += DisplayPicturesOnDashboard;
        StoryManager.EndOfDay += ResetPolaroid;
    }

    private void OnDisable() {
        StoryManager.EndOfDay -= UpdateXNodObjectPictureTakenTag;
        StoryManager.EndOfDay -= CallObjectManagerUpdateListAndSpawnObject; 
        StoryManager.EndOfDay -= DisplayPicturesOnDashboard;
        StoryManager.EndOfDay -= ResetPolaroid;
    }

    void Update()
    {
        if (!_photographyEnabled) return;

        if(_state != State.Photography) return;

        if((Input.GetKeyDown(GameInputs.PhotographyKeyCode) || Input.GetMouseButtonDown(GameInputs.PhotographyMouseButton))){
            TakePicture();
        } 
        else if (Input.GetKeyDown(GameInputs.PhotographyResetKeyCode)){
            ResetPolaroid();
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
    void UpdateXNodObjectPictureTakenTag(){
        if (_currentXnodPicturedObjects == null) return;
        _objetManager.UpdatePicturedXNodeObjets(_currentXnodPicturedObjects);
    }

    void CallObjectManagerUpdateListAndSpawnObject(){
        _objetManager.UpdateObjectAndSpawnObjectInCabine(_objetManager._objectsInCabineCount);
    }

    void DisplayPicturesOnDashboard(){ 
        Dashboard._instance.CreatePictures(_pictures);
    }
    #region Resets

    void ResetPolaroid(){
        _currentXnodPicturedObjects = new Object_XNod[_maxPicturesSlots];
        _pictures = new Picture[_maxPicturesSlots];

        _pictureTakenSlots = new bool[_maxPicturesSlots];
        for (int i =0; i < _maxPicturesSlots; i++){
            _pictureTakenSlots[i] = false;
        }
        _pictureTakensCount = 0;

        OnPolaroidReset?.Invoke();
    }
    #endregion

    IEnumerator CreatePictureScriptable(int index){
        Picture picture = ScriptableObject.CreateInstance<Picture>(); // @TODO : Save and Load this in database ?

        picture.name = _currentXnodPicturedObjects[index].name;
        StartCoroutine(picture.CreateTextureAndSprite());
        _pictures[index] = picture;

        yield return new WaitForEndOfFrame();
    }

    #region Gizmos
    void OnDrawGizmos(){
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * _photographyMaxDistance);
    }
    #endregion
}

