using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.GameEvents;

public class MeteoManager : MonoBehaviour, IGameEventManager {
    public SkyboxLerper _skyboxLerper;
    private void OnGameEvent_MeteoManager(EventName eventName, string senderName){

        Logger.LogEvent(eventName, senderName, this.GetType().Name);

        HandleCollisionEvents(eventName, senderName);
        HandleRaycastEvents(eventName, senderName);
        HandleTriggerEvents(eventName, senderName);
        HandleSpecialCases(eventName, senderName);
    }

    private void OnEndOfDay_MeteoManager(){

        if (StoryManager.LastCityVisited == Object_XNod.City.Military){
            LerpSkyboxSettingsContextMenuAB();
        } 
        else if(StoryManager.LastCityVisited == Object_XNod.City.Terraforming){
            LerpSkyboxSettingsContextMenuBA();
        }
    }

    private void Start() {
        // LerpSkyboxAllNight(); //@TODO : Lerp Skybox during all night duration, start day event, etc
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
        //Logger.LogInfo("Set rain");
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

    //Skybox Lerp - Need SkyboxConfig ?
    [ContextMenu("Lerp Skybox A->B")]
    public void LerpSkyboxSettingsContextMenuAB(){
        StartCoroutine(_skyboxLerper.LerpSkyboxSettings(_skyboxLerper._skyboxSettingsA, _skyboxLerper._skyboxSettingsB,  _skyboxLerper._timeToLerp));
    }

    [ContextMenu("Lerp Skybox B->A")]
    public void LerpSkyboxSettingsContextMenuBA(){
        StartCoroutine(_skyboxLerper.LerpSkyboxSettings(_skyboxLerper._skyboxSettingsB, _skyboxLerper._skyboxSettingsA, _skyboxLerper._timeToLerp));
    }

    [ContextMenu("Set Skybox Settings : A")]
    public void SetSkyboxSettings(){
        StartCoroutine(_skyboxLerper.LerpSkyboxSettings(_skyboxLerper._skyboxSettingsA, _skyboxLerper._skyboxSettingsA, 0.1f));
    }

    private void LerpSkyboxAllNight(){
        StartCoroutine(_skyboxLerper.LerpSkyboxSettings(_skyboxLerper._skyboxSettingsA, _skyboxLerper._skyboxSettingsA, Constants.NIGHT_DURATION));
    }
}

[System.Serializable]
public class SkyboxLerper{
    [SerializeField][Range(.5f, 30)] public float _timeToLerp;
    [SerializeField] public SkyboxSettings _skyboxSettingsA;
    [SerializeField] public SkyboxSettings _skyboxSettingsB;

    public IEnumerator LerpSkyboxSettings(SkyboxSettings A, SkyboxSettings B, float timeToLerp){

        float elapsedTime = 0;

        Material skybox = RenderSettings.skybox;
        skybox.SetTexture("_Cloud_Texture", B._cloudTexture);


        while (elapsedTime < timeToLerp){
            
            float t = elapsedTime / timeToLerp;
            //skybox.Lerp(matA, matB, t);
            
            //Sky
            skybox.SetColor("_SkyColor", Color.Lerp(A._skyColor, B._skyColor, t));
            skybox.SetColor("_HorizonColor", Color.Lerp(A._horizonColor, B._horizonColor, t));

            //Stars
            skybox.SetFloat("_Star_Density", Mathf.Lerp(A._starsDensity, B._starsDensity, t));

            //Cloud
            skybox.SetColor("_Cloud_Color", Color.Lerp(A._cloudColor, B._cloudColor, t));
            skybox.SetFloat("_Cloud_Speed", Mathf.Lerp(A._cloudSpeed, B._cloudSpeed, t));
            skybox.SetFloat("_Cloud_Height", Mathf.Lerp(A._cloudHeight, B._cloudHeight, t));
            skybox.SetFloat("_Cloud_Edge", Mathf.Lerp(A._cloudEdge, B._cloudEdge, t));

            //Astral
            skybox.SetColor("_Sun_Color", Color.Lerp(A._sunColor, B._sunColor, t));
            skybox.SetFloat("_SunSize", Mathf.Lerp(A._sunSize, B._sunSize, t));
            skybox.SetFloat("_Sun_Mask_Size", Mathf.Lerp(A._sunMaskSize, B._sunMaskSize, t));
            
            elapsedTime += Time.deltaTime;
            //RenderSettings.skybox = skybox;
            yield return null;
        }
    }
}

[System.Serializable]
public class SkyboxSettings{
    [Header ("Sky")]
    [SerializeField] public Color _skyColor;
    [SerializeField] public Color _horizonColor;

    [Header ("Stars")]
    [SerializeField][Range(0, 1000)] public float _starsDensity;

    [Header ("Clouds")]
    [SerializeField] public Texture _cloudTexture;
    [SerializeField] public Color _cloudColor;
    [SerializeField][Range(0, 1)] public float _cloudSpeed;
    [SerializeField][Range(0, 1)] public float _cloudHeight;
    [SerializeField][Range(0, 1)] public float _cloudEdge;

    [Header ("Astral Plane")]
    [ColorUsageAttribute(true,true)]
    [SerializeField] public Color _sunColor;
    [SerializeField][Range(0, 2)] public float _sunSize;
    [SerializeField][Range(0, 1)] public float _sunMaskSize;

}
