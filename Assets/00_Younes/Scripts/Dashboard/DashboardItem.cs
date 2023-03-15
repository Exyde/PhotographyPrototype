using UnityEngine;

public class DashboardItem : ScriptableObject
{
    //@TODO : Item type ? Note, text, picture, drawing... ?
    public enum ItemType {
        Note, Text, Picture, Drawing, QR, Photo //Only picture are generated !!
    } 

    [SerializeField] ItemType _itemType = ItemType.Picture;
    internal ItemType GetItemType() => _itemType;

    [SerializeField] protected Texture2D _texture;
    [SerializeField] protected Sprite _sprite;
    [SerializeField] protected Sprite _UISprite;

    [SerializeField] protected int _pictureOffsetX = 0;
    [SerializeField] protected int _pictureOffsetY = 0;

    public Texture2D GetTexture () => _texture;
    public Sprite GetSprite () => _sprite;
    public Sprite GetUISprite () => _UISprite;


}
