using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace Core.GameEvents{

    public abstract class GameEvent : MonoBehaviour {
        
        private Action<string, string> _actions;

        [Header("Event Parameters")]
        [SerializeField] string _eventName;
        [SerializeField] string _eventSender;

        [Header("On Event Objects Actions")]
        [SerializeField] UnityEvent<string, string> _events;

        [SerializeField] List<ScriptableEvents> _scriptableEvents;


        private void Awake() {
            _eventName = this.GetType().Name;
            _eventSender = this.gameObject.name;
        }
        internal void DispatchEvent(){
            _actions?.Invoke(_eventName, _eventSender);
            _events?.Invoke(_eventName, _eventSender);
        }

        bool IsEventValid(List<FactCondition> _facts){
            foreach(FactCondition f in _facts){
                //if (!CheckFact()) return false; @TODO : Fact checking in BlackboardManager
            }
            return true;
        }
    }


    [System.Serializable]
    public struct ScriptableEvents{

        [SerializeField] UnityEvent _event;
        [SerializeField] List<FactCondition> _conditions;
    }

    public struct EventContext{ //Maybe ?
        string _eventName;
        string _eventSender;
        string _contextName; //Define this ?
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





