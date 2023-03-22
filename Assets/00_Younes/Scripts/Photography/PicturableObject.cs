using UnityEngine;
using TMPro;

public class PicturableObject : MonoBehaviour
{
    [Header("Debug Preview - View Only")]
    [SerializeField] Object_XNod _xNodeObject;
    [SerializeField] Sprite _currentSprite;
    [SerializeField] Sprite _UISprite;


    [Header("References")]
    [SerializeField] TMP_Text _text_Name;

    [Header("Materials")]
    [SerializeField] Material _defaultPicturableMaterial;
    [SerializeField] Material _picturedMaterial;
    [SerializeField] Material _sabotageMaterial;
    [SerializeField] Color _color;
    MeshRenderer _renderer;

    public Object_XNod GetObject_XNod() => _xNodeObject;

    private void Awake() {
        if (_xNodeObject != null) Initialize(_xNodeObject);
    }
    public void Initialize(Object_XNod objectXNode){
        this._xNodeObject = objectXNode;

        _color = new Color(Random.Range(0f,1f), Random.Range(0f,1f), Random.Range(0f,1f), 1f);

        _renderer = GetComponent<MeshRenderer>();
        SetNonPicturedMaterial();
        //_renderer.material.SetColor("_Object_Color", _color); 
        _text_Name.text = _xNodeObject.NameOfTheObject;
    }

    //Use this later for non Picture Dashboard Component
    public Texture2D GetPictureTexture () => _xNodeObject.PictureDebugTexture;    
    public Sprite GetPictureSprite () => _currentSprite;
    public Sprite GetUISprite () => _UISprite;
    public void CreatePictureSprite(){
        Texture2D tex = GetPictureTexture();
        Rect rect = new Rect(0, 0, tex.width, tex.height);
        _currentSprite = Sprite.Create(tex, rect, new Vector2(0.5f, 0.5f), 100.0f);
    }

    public void SetNonPicturedMaterial() => _renderer.material = _defaultPicturableMaterial;
    public void SetPicturedMaterial() => _renderer.material = _picturedMaterial;
    public void SetSabotageMaterial() => _renderer.material = _sabotageMaterial;
}
