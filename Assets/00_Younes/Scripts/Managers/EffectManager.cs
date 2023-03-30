using UnityEngine;
using Core.GameEvents;
using System.Collections.Generic;

public class EffectManager : MonoBehaviour, IGameEventManager
{
    [SerializeField] ParticleSystem PS_OnPictureFlash;
    [SerializeField] ParticleSystem PH_PS_NegativeFeedback;

    [Header("Post Processing Settings")]
    [Header("Outlines Color")]
    [SerializeField] Material _outlines;
    [SerializeField] Color _indorOutlineColor;
    [SerializeField] Color _outdoorOutileColor;
    [SerializeField] List<Light> _lights;


    private void OnEnable() {
        Polaroid.OnPictureTaken += OnPictureTaken_EffectManager;
        Polaroid.OnPictureAlreadyTaken += OnPictureAlreadyTaken_EffectManager;

        Cabine._OnCabineEnter += OnCabineEnter_EffectManager;
        Cabine._OnCabineExit += OnCabineExit_EffectManager;
    }

    private void OnDisable() {
        Polaroid.OnPictureTaken -= OnPictureTaken_EffectManager;
        Polaroid.OnPictureAlreadyTaken -= OnPictureAlreadyTaken_EffectManager;

        Cabine._OnCabineEnter -= OnCabineEnter_EffectManager;
        Cabine._OnCabineExit -= OnCabineExit_EffectManager;

    }

    #region Cabine Enter And Exit
    private void OnCabineEnter_EffectManager(){
        _outlines.SetColor("_Outline_Color", _indorOutlineColor);

        // foreach(Light light in _lights){
        //     light.gameObject.SetActive(false);
        // }
    }

    private void OnCabineExit_EffectManager(){
        _outlines.SetColor("_Outline_Color", _outdoorOutileColor);
        // foreach(Light light in _lights){
        //     light.gameObject.SetActive(true);
        // }
    }
    #endregion

    #region Pictures
    private void OnPictureTaken_EffectManager(Object_XNod obj){
        PS_OnPictureFlash?.Play();
    }

    private void OnPictureAlreadyTaken_EffectManager(Object_XNod obj){
        PlayNegativeFeedBackFX(PH_PS_NegativeFeedback);
    }
    #endregion

    #region Interfaces Handlers
    public void HandleTriggerEvents(EventName eventName, string senderName)
    {
        throw new System.NotImplementedException();
    }

    public void HandleCollisionEvents(EventName eventName, string senderName)
    {
        throw new System.NotImplementedException();
    }

    public void HandleRaycastEvents(EventName eventName, string senderName)
    {
        throw new System.NotImplementedException();
    }

    public void HandleSpecialCases(EventName eventName, string senderName)
    {
        throw new System.NotImplementedException();
    }
    #endregion

    #region Particle Systems
    private void PlayNegativeFeedBackFX(ParticleSystem fx, Vector3 pos = default){
        if (fx == null) return;
        Vector3 offset = new Vector3(0, 0, -2);
        Camera cam = Camera.main;
        Instantiate(fx, cam.transform.position + cam.transform.forward * 2, Quaternion.identity);
    }
    #endregion
}
