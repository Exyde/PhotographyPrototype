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
    public Sprite _currentSprite;

    void Start()
    {
    }

    public void Initialize(Object_XNod objectXNode){
        this._xNodeObject = objectXNode;

        _color = new Color(Random.Range(0f,1f), Random.Range(0f,1f), Random.Range(0f,1f), 1f);

        _renderer = GetComponent<MeshRenderer>();
        _renderer.material.SetColor("_Object_Color", _color); 
        _text_Name.text = _xNodeObject.NameOfTheObject;
    }

    public Texture2D GetPictureTexture () => _xNodeObject.PictureDebugTexture;
    
    public Texture2D GetPictureTexture2D(){
        Texture texture = GetPictureTexture();
        Texture2D texture2D = (Texture2D)texture;
        return texture2D;
    }

    public Sprite GetPictureAsSprite(){
        Texture2D tex = GetPictureTexture();
        Rect rect = new Rect(0, 0, tex.width, tex.height);
        Sprite sprite = Sprite.Create(tex, rect, new Vector2(0.5f, 0.5f), 100.0f);
        _currentSprite = sprite;
        //return sprite;
        return _currentSprite;
    }

}
