using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackboard : MonoBehaviour
{
    public static Blackboard _instance;
    [SerializeField] GameObject _picturePrefab;
    [SerializeField] float _pictureSpawnRange;
    [SerializeField] float _pictureZOffset = 0.5f;

    [Header("List")]
    [SerializeField] List<BlackboardItemComponent> _blackboardObjects;
    [SerializeField] List<BlackboardItem> _blackboaboardObjectsDatas;

    private void Awake() {
        _instance = this;
        _blackboardObjects = new List<BlackboardItemComponent>();
        _blackboaboardObjectsDatas = new List<BlackboardItem>();
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

}
