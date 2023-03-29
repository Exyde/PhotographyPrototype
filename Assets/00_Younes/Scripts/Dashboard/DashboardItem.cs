using UnityEngine;

[CreateAssetMenu]
public class DashboardItem : ScriptableObject
{
    public enum ItemType {
        Picture, OtherElement, NarrativeBloc, UnderBloc
    } 

    [SerializeField] ItemType _itemType = ItemType.Picture;
    internal ItemType GetItemType() => _itemType;

    [SerializeField] protected Texture2D _texture;


    [SerializeField] protected Sprite _spriteBase;

    [SerializeField] protected Sprite _spriteComplete;

    [SerializeField] protected Sprite _spriteOver;

    [SerializeField] protected int _pictureOffsetX = (int) (1920/3.5f);
    [SerializeField] protected int _pictureOffsetY = (int) (1080/3.5f);

    public Texture2D GetTexture () => _texture;
    public Sprite GetSprite () => _spriteBase;


}
