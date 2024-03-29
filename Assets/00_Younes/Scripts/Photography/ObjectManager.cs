using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using System.Linq;

public class ObjectManager : MonoBehaviour
{
    public static ObjectManager _instance;

    #region Fields
    [Header ("XNod - Reference & Debug")]
    [SerializeField] ObjectToolGraph_XNod _graph;
    public List<Object_XNod> _xNodeObjectsAvailable;

    [Header ("Settings")]
    public Transform _cabineSpawnPoints;
    public int _objectsInCabineCount;

    public Material _defaultPicturableMaterial;
    public Material _picturedMaterial;
    public Material _sabotageMaterial;

    #endregion

    private void Awake() {
        if (_instance == null){
            _instance = this;
        }
        else{
            Destroy(this);
        }

        if (_cabineSpawnPoints == null){
            Logger.LogInfo("No spawn points set. Disabling Object Manager !");
            gameObject.SetActive(false);
        }
    }
    void Start()
    {
        _xNodeObjectsAvailable = new List<Object_XNod>();
        _graph.ResetGraph();
        //_xNodeObjectsAvailable = _graph.ListObjectsDisponibles;
        //Je t'enleve cette ligne je vois pas trop � quoi elle sert de base
        //Je te remplace par celle l� au cas o� t'as besoin de mettre quelque chose dans ta  list au debut, �a fait plus ou moins la meme chose :
        _xNodeObjectsAvailable = _graph.GetListOfItemsDisponibleForSpawn(1000, StoryManager.LastCityVisited);
        SpawnObjects();
    }

    public void UpdateObjectAndSpawnObjectInCabine(int objectCount){
        _xNodeObjectsAvailable = _graph.GetListOfItemsDisponibleForSpawn(objectCount, StoryManager.LastCityVisited);
        SpawnObjects();
    }

    //Clean the cabines object spawned before (can be done better) and spawn object if they are available.
    void SpawnObjects(){
        ClearTransform();

        if (_xNodeObjectsAvailable == null || _xNodeObjectsAvailable.Count <= 0) return;

        List<Transform> SpawnPoints = GetItemSpawnPositions(_objectsInCabineCount);

        for (int i = 0; i < _objectsInCabineCount; i++)
        {

            Vector3 pos = SpawnPoints[i].position;
            Quaternion rot = SpawnPoints[i].rotation;

            GameObject cabObject;

            if (_xNodeObjectsAvailable[i].MyFBX == null)
            {
                cabObject = Instantiate(_xNodeObjectsAvailable[i].PrefabObjectToSpawn, pos, rot);
                //a supprimer a terme.
            }
            else
            {
                cabObject = Instantiate(_xNodeObjectsAvailable[i].MyFBX, pos, rot);
            }

            cabObject.AddComponent<PicturableObject>().Initialize(_xNodeObjectsAvailable[i]);
            cabObject.transform.parent = this.transform;
        }
    }

    //Take the number of item to spawn and feedback an array of Vector3 Positions.
    //It copy the holder list and get random pos from it. Can be cleaner and faster.
    List<Transform> GetItemSpawnPositions(int itemCount)
    {
        int itemCountFiltered = itemCount;

        if (itemCountFiltered>= _cabineSpawnPoints.childCount)
        {
            itemCountFiltered = _cabineSpawnPoints.childCount;
        }

        List<Transform> spawnTransform = new List<Transform>();
        List<Transform> ToReturn = new();

        for (int i = 0; i < _cabineSpawnPoints.childCount; i++)
        {
            spawnTransform.Add(_cabineSpawnPoints.GetChild(i));
        }
                
        for (int i = 0; i < itemCountFiltered; i++)
        {
            int index = UnityEngine.Random.Range(0, spawnTransform.Count);
            ToReturn.Add(spawnTransform[index]);
            spawnTransform.RemoveAt(index);
        }

        return ToReturn;
    }

    /*
    //Take the number of item to spawn and feedback an array of Vector3 Positions.
    //It copy the holder list and get random pos from it. Can be cleaner and faster.
    Vector3[] GetItemSpawnPositions(int itemCount){

        List<Transform> spawnTransform = new List<Transform>(_cabineSpawnPoints.childCount);
        for (int i = 0; i < _cabineSpawnPoints.childCount; i++){
            spawnTransform.Add(_cabineSpawnPoints.GetChild(i));
        }

        Vector3[] spawnPos = new Vector3[itemCount];

        for (int i =0; i < spawnPos.Length; i++){
            int index = UnityEngine.Random.Range(0, spawnTransform.Count - 1);
            spawnPos[i] = spawnTransform[index].position;
            spawnTransform.RemoveAt(index);
        }

        return spawnPos;
    }
    */

    private void ClearTransform(){
        foreach(Transform t in this.transform){
            Destroy(t.gameObject); 
        }
    }

    public void UpdatePicturedXNodeObjets(Object_XNod[] picturedObjects){
        if (picturedObjects == null || picturedObjects.Length <= 0) return;

        foreach(Object_XNod o in picturedObjects){
            if (o != null)
                o.TakePicture();
        }
    }
}
