using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[CreateAssetMenu]
public class Graph_XNod : NodeGraph {

    public List<Object_XNod> ListObjectsDisponibles = new List<Object_XNod>();

    [ContextMenu("Mettre à jour la liste d'objets disponibles")]
    void UpdateListOfObjectDisponible()
    {
        ListObjectsDisponibles.Clear();

        List<Object_XNod> listObject = nodes.OfType<Object_XNod>().ToList();

        foreach (Object_XNod curentObject in listObject)
        {

            if (curentObject.IsDisponible())
            {
                ListObjectsDisponibles.Add(curentObject);
            }
        }
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
    }

    public  List<Object_XNod> GetListOfItemsDisponibleForSpawn(int NumberOfObjectToSpawn)
    {
        UpdateListOfObjectDisponible();

        List<Object_XNod> listObject = new List<Object_XNod>();

        int NbOfIterations = NumberOfObjectToSpawn;

        if(NbOfIterations > ListObjectsDisponibles.Count)
        {
            NbOfIterations = ListObjectsDisponibles.Count;
        }

        if (NbOfIterations == 0)
        {
            return null;
        }

        for (int i = 0; i < NbOfIterations; i++)
        {
            listObject.Add(ListObjectsDisponibles[Random.Range(0, ListObjectsDisponibles.Count)]);
        }

        return listObject;
    }

	
}