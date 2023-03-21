using UnityEngine;
using Core.GameEvents;

public class EffectManager : MonoBehaviour, IGameEventManager
{
    [SerializeField] ParticleSystem PS_OnPictureFlash;
    [SerializeField] ParticleSystem PH_PS_NegativeFeedback;

    private void OnEnable() {
        Polaroid.OnPictureTaken += OnPictureTaken_EffectManager;
        Polaroid.OnPictureAlreadyTaken += OnPictureAlreadyTaken_EffectManager;
    }

    private void OnDisable() {
        Polaroid.OnPictureTaken -= OnPictureTaken_EffectManager;
        Polaroid.OnPictureAlreadyTaken -= OnPictureAlreadyTaken_EffectManager;

    }

    private void OnPictureTaken_EffectManager(Object_XNod obj){
        PS_OnPictureFlash?.Play();
    }

    private void OnPictureAlreadyTaken_EffectManager(Object_XNod obj){
        PlayNegativeFeedBackFX(PH_PS_NegativeFeedback);
    }

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

    private void PlayNegativeFeedBackFX(ParticleSystem fx, Vector3 pos = default){
        if (fx == null) return;
        Vector3 offset = new Vector3(0, 0, -2);
        Camera cam = Camera.main;
        Instantiate(fx, cam.transform.position + cam.transform.forward * 2, Quaternion.identity);
    }
}
