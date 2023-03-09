using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class ObjectManager : MonoBehaviour
{
    #region Fields
    [Header ("XNod - Reference & Debug")]
    [SerializeField] ObjectToolGraph_XNod _graph;
    public List<Object_XNod> _xNodeObjectsAvailable;

    [Header ("Settings")]
    public Transform _cabineSpawnPoints;
    public int _objectsInCabineCount;
    #endregion
    void Start()
    {
        _graph.ResetGraph();
        //_xNodeObjectsAvailable = _graph.ListObjectsDisponibles;
        //Je t'enleve cette ligne je vois pas trop � quoi elle sert de base
        //Je te remplace par celle l� au cas o� t'as besoin de mettre quelque chose dans ta  list au debut, �a fait plus ou moins la meme chose :
        _xNodeObjectsAvailable = _graph.GetListOfItemsDisponibleForSpawn(1000, Object_XNod.City.Military);
    }

    public void UpdateObjectAndSpawnObjectInCabine(int objectCount){
        _xNodeObjectsAvailable = _graph.GetListOfItemsDisponibleForSpawn(objectCount, Object_XNod.City.Military);
        SpawnObjects();
    }

    void SpawnObjects(){
        if (_xNodeObjectsAvailable.Count <= 0) return;

        ClearTransform();
        int index = 0;

        foreach (Object_XNod item in _xNodeObjectsAvailable){ //@TODO : Add Cabine Spawns Points From Level Design
            Vector3 pos = _cabineSpawnPoints.GetChild(index).transform.position;
            GameObject picturable = Instantiate(item.PrefabObjectToSpawn, pos, Quaternion.identity);
            picturable.GetComponent<PicturableObject>().Initialize(item);
            picturable.transform.parent = this.transform;
            index++;
        }
    }

    private void ClearTransform(){
        foreach(Transform t in this.transform){
            Destroy(t.gameObject); 
        }
    }

    public void UpdatePicturedXNodeObjets(List<Object_XNod> picturedObjects){
        if (picturedObjects.Count <= 0) return;

        foreach(Object_XNod o in picturedObjects){
            if (o != null)
                o.TakePicture();
        }
    }
}
