using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class DashboardItemComponent : MonoBehaviour
{
    [SerializeField] string _name;
    [SerializeField] DashboardItem  _item;
    SpriteRenderer _renderer;
    
    [Header("Debug")]
    public Texture2D _tex;
    public Sprite _sprite;
    public KeyCode _debugPictureKeycode = KeyCode.M;

    void Start()
    {
        Initialize();
    }

    void Initialize(){
        _renderer = GetComponent<SpriteRenderer>();
        _renderer.sprite = _item.GetSprite();

        _tex = _item.GetTexture();
        _sprite = _item.GetSprite();
    }

    private void Update() {
        if (Input.GetKeyDown(_debugPictureKeycode)){
            
            if (_item.GetItemType() != DashboardItem.ItemType.Picture) return;
            //@TODO : ça pue ça ?
            Picture p = _item as Picture;
            StartCoroutine(p.CreateTextureAndSprite());

            _renderer.sprite = _item.GetSprite();
            _tex = _item.GetTexture();
            _sprite = _item.GetSprite();
        }
    }

    //@TODO : Drag n Drop

}
