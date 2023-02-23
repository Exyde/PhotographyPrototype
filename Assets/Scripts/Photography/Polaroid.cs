using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Polaroid : MonoBehaviour
{
    #region Fields
    [Header ("Settings")]
    public ObjectManager _objetManager;
    public Blackboard _blackboard;
    public bool _inCabine;
    public int _maxPicturesSlots = 3;
    public int  _pictureTakensCount = 0;
    public Object_XNod[] _currentPicturesObjects;
    public Texture[] _pictures;
    public Image[] _UIImagePictureSlots;
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

    public void TakePicture(){
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, _photographyMaxDistance, _picturableLayer)){
            PicturableObject picturable = hit.collider.gameObject.GetComponent<PicturableObject>();

            if(_pictureTakensCount < _maxPicturesSlots){
                _currentPicturesObjects[_pictureTakensCount] = picturable._xNodeObject;
                _pictures[_pictureTakensCount] = picturable.GetPictureTexture();
                _UIImagePictureSlots[_pictureTakensCount].sprite = picturable.GetPictureAsSprite();
                _pictureTakensCount++;
                _PS_Flash.Play();
            }
            else {
                HandlePictureSlotSelection();
            }
        }
    }

    void HandlePictureSlotSelection(){
        Debug.Log("Not implemented");
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
        // _selectedPicturesObjects.AddRange(_currentPicturesObjects);
        Debug.Log("Reset Polaroid.");
        for (int i = 0; i < _currentPicturesObjects.Length; i++){
            _selectedPicturesObjects.Add(_currentPicturesObjects[i]);

            _blackboard.CreatePictureOnBoard(_UIImagePictureSlots[i].sprite);
            _UIImagePictureSlots[i].sprite = null;
        }
        _objetManager.UpdatePicturedXNodeObjets(_selectedPicturesObjects);
        _pictureTakensCount = 0;

        ResetPicturesArrayAndList();
    }

    void ResetPicturesArrayAndList(){
        // Debug.Log("Reseting Arrays");

        _selectedPicturesObjects.Clear();
        _currentPicturesObjects = new Object_XNod[_maxPicturesSlots];
        _pictures = new Texture[_maxPicturesSlots];

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
