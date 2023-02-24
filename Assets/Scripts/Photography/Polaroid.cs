using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class Polaroid : MonoBehaviour
{
    #region Fields
    [Header ("Settings")]
    public ObjectManager _objetManager;
    public Blackboard _blackboard;
    public bool _inCabine;
    public int _maxPicturesSlots = 3;
    public bool[] _pictureTakenSlots;
    public int  _pictureTakensCount = 0;
    public Object_XNod[] _currentPicturesObjects;
    public Texture[] _pictures;
    public Image[] _UIImagePictureSlots;
    public TMP_Text _pictureOverrideTxt;
    public List<Object_XNod> _selectedPicturesObjects = new List<Object_XNod>();

    [Header ("Events")]
    [HideInInspector] public UnityEvent _OnCabineEnter;
    [HideInInspector] public UnityEvent _OnCabineExit;

    [Header("Photgraphy Mecanic")]
    [SerializeField] LayerMask _picturableLayer;
    // [SerializeField] string _picturableTag = "Picturable";
    [SerializeField][Range(1, 20)] float _photographyMaxDistance = 10f;
    [SerializeField] ParticleSystem _PS_Flash;
    #endregion

    #region UnityCallbacks
    void Start(){
        ResetPicturesArrayAndList();

        _OnCabineExit.AddListener(ResetPolaroid);
        _OnCabineExit.AddListener(CallManagerUpdateList);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P) || Input.GetMouseButtonDown(0)){
            TakePicture();
        }
    }
    #endregion

    public void TakePicture(){ //Can't do this with index incrementation like this, we need to have custom slots @TODO
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, _photographyMaxDistance, _picturableLayer)){
            PicturableObject picturable = hit.collider.gameObject.GetComponent<PicturableObject>();

            if(_pictureTakensCount < _maxPicturesSlots){
                int slotIndex = GetAvailableSlotIndex();
                Debug.Log("Slot index : " + slotIndex);

                if (slotIndex >= 0){ //If a slot is available
                    _currentPicturesObjects[slotIndex] = picturable._xNodeObject;
                    _pictures[slotIndex] = picturable.GetPictureTexture();
                    _UIImagePictureSlots[slotIndex].sprite = picturable.GetPictureAsSprite();
                    _pictureTakenSlots[slotIndex] = true;

                    _pictureTakensCount++;
                    _PS_Flash.Play();
                }

            }
            else {
                StartCoroutine(HandlePictureSlotSelection());
            }
        }
    }

    IEnumerator HandlePictureSlotSelection(){
        _pictureOverrideTxt.gameObject.SetActive(true);
        //Disable movement & Stuff ? Coroutine ? While ?
        int playerPictureSlotIndexSelected = -1;

        
        while(playerPictureSlotIndexSelected == -1){
            if (Input.GetKeyDown(KeyCode.I)) playerPictureSlotIndexSelected = 0;
            else if (Input.GetKeyDown(KeyCode.I)) playerPictureSlotIndexSelected = 1;
            else if (Input.GetKeyDown(KeyCode.I)) playerPictureSlotIndexSelected = 2;
            Debug.Log("In While");
            yield return new WaitForSeconds(.1f);
        }

        _pictureTakenSlots[playerPictureSlotIndexSelected] = false;
        _currentPicturesObjects[playerPictureSlotIndexSelected] = null;
        _pictures[playerPictureSlotIndexSelected] = null;
        _UIImagePictureSlots[playerPictureSlotIndexSelected].sprite = null;

        _pictureOverrideTxt.gameObject.SetActive(false);
        TakePicture();

    }

    private int GetAvailableSlotIndex(){
        for (int i = 0; i < _maxPicturesSlots; i++){
            if (_pictureTakenSlots[i] == false) return i;
        }
        return -1;
    }
    
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Cabine"){
            _inCabine = true;
            _OnCabineEnter?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Cabine"){
            _inCabine = false;
            _OnCabineExit?.Invoke();
        } 
    }

    void ResetPolaroid(){
        Debug.Log("Reset Polaroid.");
        for (int i = 0; i < _currentPicturesObjects.Length; i++){
            _selectedPicturesObjects.Add(_currentPicturesObjects[i]);
        }

        for (int i =0; i < _maxPicturesSlots; i++){
            if (_UIImagePictureSlots[i].sprite != null){
                _blackboard.CreatePictureOnBoard(_UIImagePictureSlots[i].sprite);
                _UIImagePictureSlots[i].sprite = null;
            }
        }
        
        _objetManager.UpdatePicturedXNodeObjets(_selectedPicturesObjects);

        ResetPicturesArrayAndList();
    }

    void ResetPicturesArrayAndList(){
        // Debug.Log("Reseting Arrays");

        _selectedPicturesObjects.Clear();
        _currentPicturesObjects = new Object_XNod[_maxPicturesSlots];
        _pictures = new Texture[_maxPicturesSlots];

        //One with remplace this second
        _pictureTakenSlots = new bool[_maxPicturesSlots];
        for (int i =0; i < _maxPicturesSlots; i++){
            _pictureTakenSlots[i] = false;
        }
        _pictureTakensCount = 0;
        // Debug.Log("Finish Reseting Arrays");
    }

    void CallManagerUpdateList(){
        _objetManager.UpdateObjectList(_objetManager._objectsInCabineCount);
    }

    void OnDrawGizmos(){
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * _photographyMaxDistance);
    }
}
