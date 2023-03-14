
namespace Core.GameEvents{
    public interface IGameEventManager{
        public void HandleTriggerEvents(EventName eventName, string senderName);
        public void HandleCollisionEvents(EventName eventName, string senderName);
        public void HandleRaycastEvents(EventName eventName, string senderName); //Picture, Look, Interact

        public void HandleSpecialCases(EventName eventName, string senderName);
    }
}