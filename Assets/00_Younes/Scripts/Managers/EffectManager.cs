using UnityEngine;
using Core.GameEvents;

public class EffectManager : MonoBehaviour, IGameEventManager
{
    [SerializeField] ParticleSystem PS_OnPictureFlash;
    private void OnEnable() {
        Polaroid.OnPictureTaken += OnPictureTaken_EffectManager;
    }

    private void OnDisable() {
        Polaroid.OnPictureTaken -= OnPictureTaken_EffectManager;
    }

    private void OnPictureTaken_EffectManager(Object_XNod obj){
        PS_OnPictureFlash?.Play();
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
}
