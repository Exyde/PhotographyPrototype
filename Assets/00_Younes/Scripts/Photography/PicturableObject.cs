using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using TMPro;

public class PicturableObject : MonoBehaviour
{
    [Header("Debug Preview - View Only")]
    [SerializeField] Object_XNod _xNodeObject;
    [SerializeField] Sprite _currentSprite;
    [SerializeField] Sprite _UISprite;

    [SerializeField] Color _color;

    [Header("References")]
    [SerializeField] TMP_Text _text_Name;
    MeshRenderer _renderer;

    public Object_XNod GetObject_XNod() => _xNodeObject;

    public void Initialize(Object_XNod objectXNode){
        this._xNodeObject = objectXNode;

        _color = new Color(Random.Range(0f,1f), Random.Range(0f,1f), Random.Range(0f,1f), 1f);

        _renderer = GetComponent<MeshRenderer>();
        _renderer.material.SetColor("_Object_Color", _color); 
        _text_Name.text = _xNodeObject.NameOfTheObject;
    }

    public Texture2D GetPictureTexture () => _xNodeObject.PictureDebugTexture;    
    public Sprite GetPictureAsSprite(){
        Texture2D tex = GetPictureTexture();
        Rect rect = new Rect(0, 0, tex.width, tex.height);
        Sprite sprite = Sprite.Create(tex, rect, new Vector2(0.5f, 0.5f), 100.0f);
        _currentSprite = sprite;

        //return sprite;
        return _currentSprite;
    }

}
