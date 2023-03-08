using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Dashboard/Picture", fileName = "New Picture")]
public class Picture : DashboardItem
{
    public IEnumerator CreateTextureAndSprite(){

        yield return new WaitForEndOfFrame();
        _texture = new Texture2D(Screen.width - 2 * _pictureOffsetX, Screen.height - 2 * _pictureOffsetY, TextureFormat.RGB24, false);

        Rect regionToRead = new Rect(0 + _pictureOffsetX, 0 + _pictureOffsetY, Screen.width - _pictureOffsetX, Screen.height - _pictureOffsetY);

        _texture.ReadPixels(regionToRead, 0, 0, false);
        _texture.Apply();

        CreateSprite();
    }
    
    protected void CreateSprite(){
        Sprite sprite = Sprite.Create(_texture, new Rect(0, 0, _texture.width, _texture.height), new Vector2(0.5f, 0.5f), 100.0f);
        _sprite = sprite;
    }

    public void SavePictureTexture(){
        SaveSystem.SaveTexToPng(_texture, name, Random.Range(0, 256)); //@TODO : Remove this index
    }
}

