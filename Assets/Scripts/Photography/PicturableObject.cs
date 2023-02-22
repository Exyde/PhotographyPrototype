using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using TMPro;

public class PicturableObject : MonoBehaviour
{
    [SerializeField] public Object_XNod _xNodeObject;
    [SerializeField] TMP_Text _text_Name;

    MeshRenderer _renderer;
    public Color _color;

    void Start()
    {
    }

    public void Initialize(Object_XNod objectXNode){
        this._xNodeObject = objectXNode;

        _renderer = GetComponent<MeshRenderer>();
        _renderer.material.SetColor("_Object_Color", _color); 
        _text_Name.text = _xNodeObject.NameOfTheObject;
    }

    public Texture GetPictureTexture () => _xNodeObject.PictureDebugTexture;



}
