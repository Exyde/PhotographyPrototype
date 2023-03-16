using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Dashboard : MonoBehaviour
{
    public static Dashboard _instance;
    [SerializeField] GameObject _picturePrefab;
    [SerializeField] float _pictureSpawnRange;
    [SerializeField] float _pictureZOffset = 0.5f;

    [Header("List")]
    [SerializeField] List<DashboardItemComponent> _dashboardObjects;
    [SerializeField] List<DashboardItem> _blackboaboardObjectsDatas;

    private void Awake() {
        _instance = this;
        _dashboardObjects = new List<DashboardItemComponent>();
        _blackboaboardObjectsDatas = new List<DashboardItem>();
    }

    public void CreatePictureOnBoard(Sprite sprite) //@TODO : Other implementation
    {
        //Position it on the board? Where ?
        Vector3 pos = transform.position + UnityEngine.Random.insideUnitSphere * _pictureSpawnRange;
        pos.z = transform.position.z + _pictureZOffset;

        //Create prefab Instance
        GameObject picture = Instantiate(_picturePrefab, pos, Quaternion.identity);
        picture.transform.parent = this.transform;

        //Initialize with sprite
        picture.GetComponentInChildren<SpriteRenderer>().sprite = sprite;
    }

    public void  CreatePictures(Picture[] pictures){
        foreach (Picture pict in pictures){
            if (pict != null){
                CreatePictureOnBoard(pict.GetSprite());
                //pict.SavePictureTexture(); //Move this again ? XD
            }
        }
    }

}
