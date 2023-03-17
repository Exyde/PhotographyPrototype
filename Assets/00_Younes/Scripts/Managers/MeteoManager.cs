using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.GameEvents;

public class MeteoManager : MonoBehaviour, IGameEventManager {

    public Material _militarySkybox;
    public Material _terraformingSkybox;

    private void OnGameEvent_MeteoManager(EventName eventName, string senderName){

        Logger.LogEvent(eventName, senderName, this.GetType().Name);

        HandleCollisionEvents(eventName, senderName);
        HandleRaycastEvents(eventName, senderName);
        HandleTriggerEvents(eventName, senderName);
        HandleSpecialCases(eventName, senderName);
    }

    private void OnEndOfDay_MeteoManager(){

        if (StoryManager.LastCityVisited == Object_XNod.City.Military){
            RenderSettings.skybox = _militarySkybox;
        } 
        else if(StoryManager.LastCityVisited == Object_XNod.City.Terraforming){
            RenderSettings.skybox = _terraformingSkybox;
        }
    }

    private void OnEnable() {
        GameEvent._onGameEvent += OnGameEvent_MeteoManager;
        StoryManager.EndOfDay += OnEndOfDay_MeteoManager;

    }

    private void OnDisable() {
        GameEvent._onGameEvent += OnGameEvent_MeteoManager;  
        StoryManager.EndOfDay -= OnEndOfDay_MeteoManager;
    }

    public void RequestRain(){
        Logger.LogInfo("Set rain"); //@Todo Add Log Event Action
    }

    public void HandleTriggerEvents(EventName eventName, string senderName)
    {        
        if (eventName == EventName.TRIGGER_ENTER){
            RequestRain();
        }
        else if (eventName ==  EventName.TRIGGER_EXIT){
            Logger.LogInfo("Setting Sun !");
        }
    }

    public void HandleCollisionEvents(EventName eventName, string senderName)
    {

    }

    public void HandleRaycastEvents(EventName eventName, string senderName)
    {
    }

    public void HandleSpecialCases(EventName eventName, string senderName){
        if (senderName == "Cube_OnTriggerExit"){
            Logger.LogInfo("Get ready for the storm");
        }
    }
}
