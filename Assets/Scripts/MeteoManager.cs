using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.GameEvents;

public class MeteoManager : MonoBehaviour
{
    void OnGameEvent_MeteoManager(string eventName, string eventSender){
        
        if (eventName == "OnTriggerEnterEvent"){
            Logger.LogInfo("Setting Rain !");
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
}
