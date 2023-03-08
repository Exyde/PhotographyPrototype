using UnityEngine;
using System;
using System.Collections.Generic;

namespace Core.GameEvents{ //Add class EventContext ?

    public class GameEvent : MonoBehaviour {
        
        [SerializeField] Action<string, string, Conditions> _action;
        [SerializeField] Conditions _conditions;
        internal virtual void DispatchEvent(string eventName, string context, Conditions conditions = default){
            _action?.Invoke(eventName, context, conditions);
        }
    }

    [System.Serializable]
    public struct Conditions{
        [SerializeField] List<Condition> _conditions;
    }

    [System.Serializable]
    public struct Condition{
        [SerializeField] string _key;
        [SerializeField] int _value;
    }

    public class EventContext{
        string _eventName;
        string _contextName;
        Conditions _conditions;
    }
}
