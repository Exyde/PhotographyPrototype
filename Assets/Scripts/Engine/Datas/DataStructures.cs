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

    public enum Comparaison{
            Equal, Different, Superior, SuperiorOrEqual, Inferior, InferiorOrEqual
        }
}
