using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[CreateAssetMenu(fileName = "Object Tool", menuName = "Our Tools/Object Tool", order = 1)]
public class ObjectToolGraph_XNod : NodeGraph {

    List<Object_XNod> getListOfObjectDisponible()
    {
        List<Object_XNod> listObjectsDisponibles = new List<Object_XNod>();

        List<Object_XNod> listObject = nodes.OfType<Object_XNod>().ToList();

        foreach (Object_XNod curentObject in listObject)
        {
            if (curentObject.IsDisponible() && !curentObject.IsStaticObject)
            {
                listObjectsDisponibles.Add(curentObject);
            }
        }

        return listObjectsDisponibles;
    }

    List<Object_XNod> getListOfObjectFilteredByCity(List<Object_XNod> listObject, Object_XNod.City city)
    {
        List<Object_XNod> ObjectToDelete = new();

        foreach(Object_XNod curentObject in listObject)
        {
            if(curentObject.FromCity != Object_XNod.City.DontMatter && curentObject.FromCity != city)
            {
                ObjectToDelete.Add(curentObject);
            }
        }

        foreach(Object_XNod curentObject in ObjectToDelete)
        {
            listObject.Remove(curentObject);
        }

        return listObject;
    }

    [ContextMenu("Reset les états de départ des objets")]
    public void ResetGraph()
    {
        List<Object_XNod> listObject = nodes.OfType<Object_XNod>().ToList();
        foreach (Object_XNod curentObject in listObject)
        {
            curentObject.ResetObject();
        }

        List<NarrativeBloc_XNode> listBloc = nodes.OfType<NarrativeBloc_XNode>().ToList();
        foreach (NarrativeBloc_XNode curentObject in listBloc)
        {
            curentObject.ResetNarrativeBloc();
        }

        List<UnderBloc_XNode> listUnderBloc = nodes.OfType<UnderBloc_XNode>().ToList();
        foreach (UnderBloc_XNode curentObject in listUnderBloc)
        {
            curentObject.ResetUnderBloc();
        }
    }

    public  List<Object_XNod> GetListOfItemsDisponibleForSpawn(int numberOfObjectToSpawn, Object_XNod.City lastCityVisited)
    {
        List<Object_XNod> listObjectsDisponibles = getListOfObjectDisponible();

        listObjectsDisponibles = getListOfObjectFilteredByCity(listObjectsDisponibles, lastCityVisited);

        List<Object_XNod> myListObject = new List<Object_XNod>();

        int NbOfIterations = numberOfObjectToSpawn;

        if(NbOfIterations > listObjectsDisponibles.Count)
        {
            NbOfIterations = listObjectsDisponibles.Count;
        }

        if (NbOfIterations == 0)
        {
            return null;
        }

        for (int i = 0; i < NbOfIterations; i++)
        {
            int RandomInList = Random.Range(0, listObjectsDisponibles.Count);
            myListObject.Add(listObjectsDisponibles[RandomInList]);
            listObjectsDisponibles.RemoveAt(RandomInList);
        }
        getListOfObjectDisponible();

        return myListObject;
    }

	
}