using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.GameEvents;

public class DataManager : MonoBehaviour
{
    public static DataManager DM;

    Data_XNode Datas;

    Dictionary<string, BlackBoard> DictionaryToBlackBoard = new();



    void Awake()
    {
        if (DM == null)
        {
            DM = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this);
        }

        foreach (BlackBoard curentBlackboard in Datas.ListBlackboard)
        {
            DictionaryToBlackBoard.Add(curentBlackboard.BlackboardName, curentBlackboard);

            curentBlackboard.OnStart();
        }

    }

    public Fact GetFact(string blackboardName, string factName)
    {
        BlackBoard blackboard;

        if(!DictionaryToBlackBoard.TryGetValue(blackboardName, out blackboard))
        {
            Debug.Log("Le blackboard '" + blackboardName + "' n'existe pas mais a �t� r�clam�.");
            return null;
        }

        Fact fact;

        if(!blackboard.DictionaryToFact.TryGetValue(factName, out fact))
        {
            Debug.Log("Le fact '" + blackboardName + "' n'existe pas dans le blackboard '" + blackboardName + "' mais a �t� r�clam�.");
            return null;
        }

        return fact;
    }

    public int GetFactValue(string blackboardName, string factName)
    {
        return GetFact(blackboardName, factName).FactValue;
    }

    public void SetFactValue(string blackboardName, string factName, Operation operation, int value)
    {
        Fact fact = GetFact(blackboardName, factName);

        switch (operation)
        {
            case Operation.SetTo:
                fact.FactValue = value;
                break;

            case Operation.Add:
                fact.FactValue += value;
                break;

            case Operation.Substract:
                fact.FactValue -= value;
                break;
        }
    }

    public void SetFactValue(Fact fact, Operation operation, int value)
    {
        switch (operation)
        {
            case Operation.SetTo:
                fact.FactValue = value;
                break;

            case Operation.Add:
                fact.FactValue += value;
                break;

            case Operation.Substract:
                fact.FactValue -= value;
                break;
        }
    }

    public bool CompareFactValueTo(string blackboardName, string factName, Comparaison comparaison, int value)
    {
        int factValue = GetFactValue(blackboardName, factName);

        switch (comparaison)
        {
            case Comparaison.Equal:
                return factValue == value;

            case Comparaison.Different:
                return factValue != value;

            case Comparaison.Superior:
                return factValue > value;

            case Comparaison.SuperiorOrEqual:
                return factValue >= value;

            case Comparaison.Inferior:
                return factValue < value;

            case Comparaison.InferiorOrEqual:
                return factValue <= value;

            default:
                return false;
        }

        
    }

    public bool CompareFactValueTo(Fact fact, Comparaison comparaison, int value)
    {
        int factValue = fact.FactValue;

        switch (comparaison)
        {
            case Comparaison.Equal:
                return factValue == value;

            case Comparaison.Different:
                return factValue != value;

            case Comparaison.Superior:
                return factValue > value;

            case Comparaison.SuperiorOrEqual:
                return factValue >= value;

            case Comparaison.Inferior:
                return factValue < value;

            case Comparaison.InferiorOrEqual:
                return factValue <= value;

            default:
                return false;
        }


    }



}
