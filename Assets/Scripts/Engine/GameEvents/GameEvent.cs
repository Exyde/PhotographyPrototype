using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace Core.GameEvents{

    public abstract class GameEvent : MonoBehaviour {
        
        //Core Game Action
        public static Action<string, string> _onGameEvent;

        [Header("Event Parameters - Rien a rentrer ici ._. - juste debug !")]
        [SerializeField] string _eventName;
        [SerializeField] string _eventSender;

        [Header("Scriptable Events for Designers <3")]
        [SerializeField] List<ScriptableEvents> _scriptableEvents;

        private void Awake() {
            _eventName = this.GetType().Name;
            _eventSender = this.gameObject.name;
        }
        internal virtual void DispatchEvent(){
            Logger.LogInfo("Event Dispatched : " + _eventName + " | Event Sender : " + _eventSender);
            _onGameEvent?.Invoke(_eventName, _eventSender);

            foreach(ScriptableEvents e in _scriptableEvents){
                if (IsEventValid(e.GetFacts())) e.GetEvent()?.Invoke();
            }
        }

        bool IsEventValid(List<FactCondition> _facts){
            foreach(FactCondition f in _facts){
                //if (!CheckFact()) return false; @TODO : Fact checking in BlackboardManager
                BlackboardManager.BBM.CompareFactValueTo(f);
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
        public List<FactCondition> GetFacts() => _conditions;

    }

    public struct EventContext{ //Maybe ?
        string _eventName;
        string _eventSender;
        string _contextName; //Define this ?
    }
}





