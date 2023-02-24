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

    private void Awake() {
        _instance = this;
    }

    public void CreatePictureOnBoard(Sprite sprite)
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
