using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[NodeWidth(600)]
[CreateNodeMenu("Data/Data Node")]

public class Data_XNode : Node
{
    public string User;

    public List<BlackBoard> ListBlackboard;

    protected override void Init()
    {
        base.Init();
    }

    
}

[System.Serializable]
public class BlackBoard : ISerializationCallbackReceiver
{
    [HideInInspector] public string name;

    public string BlackboardName;

    public List<Fact> Facts;

    public Dictionary<string, Fact> DictionaryToFact = new();

    public void OnBeforeSerialize()
    {
        name = "Blackboard " + BlackboardName;
    }

    public void OnAfterDeserialize()
    {
        name = "Blackboard  " + BlackboardName;
    }

    public void OnStart()
    {
        foreach (Fact curentFact in Facts)
        {
            DictionaryToFact.Add(curentFact.FactName, curentFact);
        }
    }

}

[System.Serializable]
public class Fact : ISerializationCallbackReceiver
{
    [HideInInspector] public string name;

    public string FactName;

    public int FactValue;

    public void OnBeforeSerialize()
    {
        name = FactName;
    }

    public void OnAfterDeserialize()
    {
        name = FactName;
    }

}
