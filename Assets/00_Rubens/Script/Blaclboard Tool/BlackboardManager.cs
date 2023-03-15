using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.GameEvents;

public class BlackboardManager : MonoBehaviour
{
    public static BlackboardManager BBM;

    [SerializeField] List<Blackboard_XNode> Datas;

    Dictionary<string, BlackBoard> DictionaryToBlackBoard = new();



    void Awake()
    {
        if (BBM == null)
        {
            BBM = this;

            //DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this);
        }

        foreach(Blackboard_XNode curentNode in Datas)
        {
            foreach (BlackBoard curentBlackboard in curentNode.ListBlackboard)
            {
                DictionaryToBlackBoard.Add(curentBlackboard.BlackboardName, curentBlackboard);

                curentBlackboard.OnStart();
            }
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
        return GetFact(blackboardName, factName).CurrentFactValue;
    }


    public void SetFactValue(Fact fact, Operation operation, int value)
    {
        switch (operation)
        {
            case Operation.SetTo:
                fact.CurrentFactValue = value;
                break;

            case Operation.Add:
                fact.CurrentFactValue += value;
                break;

            case Operation.Substract:
                fact.CurrentFactValue -= value;
                break;
        }
    }

    public void SetFactValue(string blackboardName, string factName, Operation operation, int value)
    {
        Fact fact = GetFact(blackboardName, factName);

        SetFactValue(fact, operation, value);
    }

    public void SetFactValue(FactOperation factOperation)
    {
        SetFactValue(factOperation._blackboardName, factOperation._factName, factOperation._operation, factOperation._value);
    }

    public void SetFactValue(List<FactOperation> factsOperation)
    {
        foreach(FactOperation fo in factsOperation)
        {
            SetFactValue(fo._blackboardName, fo._factName, fo._operation, fo._value);
        }
    }


    public bool CompareFactValueTo(Fact fact, Comparaison comparaison, int value)
    {
        int factValue = fact.CurrentFactValue;

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

    public bool CompareFactValueTo(string blackboardName, string factName, Comparaison comparaison, int value)
    {
        Fact fact = GetFact(blackboardName, factName);

        return CompareFactValueTo(fact, comparaison, value);
    }

    public bool CompareFactValueTo(FactCondition factCondition)
    {
        return CompareFactValueTo(factCondition._blackboardName, factCondition._factName, factCondition._comparaison, factCondition._value);
    }

    public bool CompareFactValueTo(List<FactCondition> factsCondition)
    {
        foreach(FactCondition fc in factsCondition)
        {
            if(!CompareFactValueTo(fc._blackboardName, fc._factName, fc._comparaison, fc._value))
            {
                return false;
            }
        }

        return true;
    }

}
