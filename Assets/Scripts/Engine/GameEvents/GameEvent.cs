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

        [Header("Scriptable Events for Designers <3")]
        [SerializeField] List<ScriptableEvents> _scriptableEvents;

        private void Awake() {
            _eventName = this.GetType().Name;
            _eventSender = this.gameObject.name;
        }
        internal virtual void DispatchEvent(){
            _actions?.Invoke(_eventName, _eventSender);
            foreach(ScriptableEvents e in _scriptableEvents){
                e.GetEvent()?.Invoke();
            }
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
        [SerializeField] string _eventName;
        [SerializeField] UnityEvent _event;
        [SerializeField] List<FactCondition> _conditions;

        public UnityEvent GetEvent() => _event;
    }

    public struct EventContext{ //Maybe ?
        string _eventName;
        string _eventSender;
        string _contextName; //Define this ?
    }
}





