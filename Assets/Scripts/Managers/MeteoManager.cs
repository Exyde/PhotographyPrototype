using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.GameEvents;

public class MeteoManager : MonoBehaviour, IGameEventManager {

    private void OnGameEvent_MeteoManager(string eventName, string eventSender){

        HandleCollisionEvents(eventName, eventSender);
        HandleRaycastEvents(eventName, eventSender);
        HandleTriggerEvents(eventName, eventSender);

        if (eventName == "OnTriggerEnterEvent"){
            RequestRain();
        }

        else if (eventName == "OnTriggerExitEvent"){
            Logger.LogInfo("Setting Sun !");
        }

        else if (eventSender == "Cube_OnTriggerExit"){
            Logger.LogInfo("Get ready for the storm");
        }

        else{
            Logger.LogInfo("Not handled event :3");
        }
    }

    private void OnEnable() {
        GameEvent._onGameEvent += OnGameEvent_MeteoManager;
    }

    private void OnDisable() {
        GameEvent._onGameEvent += OnGameEvent_MeteoManager;  
    }

    public void RequestRain(){
        Logger.LogInfo("Set rain");
    }

    public void HandleTriggerEvents(string eventName, string senderName)
    {
        return;
        throw new System.NotImplementedException();
    }

    public void HandleCollisionEvents(string eventName, string senderName)
    {
        throw new System.NotImplementedException();
    }

    public void HandleRaycastEvents(string eventName, string senderName)
    {
        throw new System.NotImplementedException();
    }
}
