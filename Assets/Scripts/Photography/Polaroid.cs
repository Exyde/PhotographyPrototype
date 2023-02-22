using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Polaroid : MonoBehaviour
{
    [Header ("Settings")]
    public ObjectManager _objetManager;
    public bool _inCabine;
    public int _maxSlots = 3;
    public int  _pictureTakens = 0;
    public List<Object_XNod> _currentPictures;

    [Header ("Events")]
    public UnityEvent _OnCabineEnter;
    public UnityEvent _OnCabineExit;

    [Header("Photgrapgy Mecanic")]
    [SerializeField] LayerMask _picturableLayer;
    [SerializeField] string _picturableTag = "Picturable";


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            TakePicture();
        }
    }

    void Start(){
        _OnCabineExit.AddListener(ResetPolaroid);
        _OnCabineExit.AddListener(CallManagerUpdateList);
    }

    public void TakePicture(){
        // Physics.Raycast()



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
        _objetManager.UpdatePicturedXNodeObjets(_currentPictures);
        _pictureTakens = 0;
        _currentPictures.Clear();
    }

    void CallManagerUpdateList(){
        _objetManager.UpdateObjectList(_objetManager._objectsInCabineCount);
    }
}
