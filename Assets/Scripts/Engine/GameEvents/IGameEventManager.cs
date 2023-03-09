
namespace Core.GameEvents{
    public interface IGameEventManager{
        public void HandleTriggerEvents(string eventName, string senderName);
        public void HandleCollisionEvents(string eventName, string senderName);
        public void HandleRaycastEvents(string eventName, string senderName); //Picture, Look, Interact
    }
}