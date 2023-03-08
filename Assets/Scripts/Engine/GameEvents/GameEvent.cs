using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace Core.GameEvents{

    public abstract class GameEvent : MonoBehaviour {
        
        private Action<string, string> _actions;

        [Header("Event Parameters")]
        [SerializeField] string _eventName;
        [SerializeField] string _eventContext;

        [Header("On Event Objects Actions")]
        [SerializeField] UnityEvent<string, string> _events;

        [SerializeField] List<GEvent> _gvents;
        internal void DispatchEvent(){
            _actions?.Invoke(_eventName, _eventContext);
            _events?.Invoke(_eventName, _eventContext);
        }

        bool IsEventValid(List<FactCondition> _facts){
            foreach(FactCondition f in _facts){
                //if (!CheckFact()) return false; @TODO : Fact checking in BlackboardManager
            }
            return true;
        }
    }


    [System.Serializable]
    public struct GEvent{

        [SerializeField] UnityEvent _event;
        [SerializeField] List<FactCondition> _conditions;
    }

    public class EventContext{
        string _eventName;
        string _contextName;
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





