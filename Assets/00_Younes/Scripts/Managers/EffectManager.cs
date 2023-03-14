using UnityEngine;
using Core.GameEvents;

public class EffectManager : MonoBehaviour, IGameEventManager
{
    private void OnEnable() {
        Polaroid.OnPictureTaken += OnPictureTaken_EffectManager;
    }

    private void OnDisable() {
        Polaroid.OnPictureTaken -= OnPictureTaken_EffectManager;
    }

    private void OnPictureTaken_EffectManager(Object_XNod obj){
        Debug.Log("Flash fx for" + obj.NameOfTheObject);
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
