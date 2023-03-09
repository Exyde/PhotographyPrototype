using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace Core.GameEvents{
    public abstract class GameEvent : MonoBehaviour {
        
        //Core Game Action - Static, and registered by ALL the managers ?
        public static Action<string, string> _onGameEvent;

        [Header("Event Parameters - Rien a rentrer ici ._. - juste debug !")]
        [SerializeField] EventName _eventName;
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
                if (IsEventValid(e.GetFactsConditions())){
                    Logger.LogInfo("Valid Fact => Triggering Event : " + _eventName);

                    e.GetEvent()?.Invoke();

                    //@TODO-RUBENS : Update with BBM taking a FactList
                    foreach (FactOperation operation in e.GetFactsOperations()){
                        BlackboardManager.BBM.SetFactValue(operation);
                    }
                }
            }
        }

        bool IsEventValid(List<FactCondition> _facts){
            foreach(FactCondition factCondition in _facts){
                bool test = BlackboardManager.BBM.CompareFactValueTo(factCondition);
                if (test == false){
                    Logger.LogInfo("Failed at fact XXX - To Implement Better");
                    return false;
                }
            }
            return true;
        }
    }

    [System.Serializable]
    public struct ScriptableEvents{
        [SerializeField] string _eventName;
        [SerializeField] UnityEvent _event;
        [SerializeField] List<FactOperation> _factOperations;
        [SerializeField] List<FactCondition> _factConditions;


        public UnityEvent GetEvent() => _event;
        public List<FactCondition> GetFactsConditions() => _factConditions;
        public List<FactOperation> GetFactsOperations() => _factOperations;


    }

    public struct EventContext{ //Maybe ?
        EventName _eventName;
        string _eventSender;
        string _contextName; //Define this ?
    }
}





