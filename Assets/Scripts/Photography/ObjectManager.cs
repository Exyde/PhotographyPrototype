using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public class ObjectManager : MonoBehaviour
{
    [SerializeField] Graph_XNod _graph;

    public List<Object_XNod> _xNodeObjectsAvailable;

    public Transform _cabineSpawnPoints;

    public int _objectsInCabineCount;

    void Start()
    {
        _graph.ResetGraph();
        _xNodeObjectsAvailable = _graph.ListObjectsDisponibles;
    }

    public void UpdateObjectList(int objectCount){
        _xNodeObjectsAvailable = _graph.GetListOfItemsDisponibleForSpawn(objectCount);
        SpawnObjects();
    }

    void SpawnObjects(){
        foreach(Transform t in this.transform){
            Destroy(t.gameObject); 
        }
        int index = 0;

        foreach (Object_XNod item in _xNodeObjectsAvailable){
            Vector3 pos = _cabineSpawnPoints.GetChild(index).transform.position;
            GameObject picturable = Instantiate(item.PrefabObjectToSpawn, pos, Quaternion.identity);
            picturable.GetComponent<PicturableObject>().Initialize(item);
            picturable.transform.parent = this.transform;
            index++;
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
