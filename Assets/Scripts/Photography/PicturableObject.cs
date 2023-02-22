using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using TMPro;
public class PicturableObject : MonoBehaviour
{
    [SerializeField] Object_XNod _xNodeObject;
    [SerializeField] TMP_Text _text_Name;

    MeshRenderer _renderer;
    public Color _color;

    void Start()
    {
        _renderer = GetComponent<MeshRenderer>();
        _renderer.material.SetColor("_Object_Color", _color); 

        _text_Name.text = _xNodeObject.NameOfTheObject;
    }



}
