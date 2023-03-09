using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.GameEvents{
    public static class DataStructures{

    }

    [System.Serializable]
    public struct FactCondition{
            [SerializeField] string _blackboardName;
            [Space(5)]
            [SerializeField] string _factName;
            [SerializeField] Comparaison _comparaison;
            [SerializeField] int _value;
        }

    public struct FactOperation{
        [SerializeField] string _blackboardName;
        [Space(5)]
        [SerializeField] string _factName;
        [SerializeField] Operation _operation;
        [SerializeField] int _value;
    }

    
    public enum Operation {SetTo, Add, Substract };
    public enum Comparaison { Equal, Different, Superior, SuperiorOrEqual, Inferior, InferiorOrEqual };



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
}
