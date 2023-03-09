using UnityEngine; //@TODO : Add Stay timing behavior here

namespace Core.GameEvents{
    public class OnTriggerStayEvent : OnTriggerGameEvent{
   
        protected override void SetEventType()
        {
            _eventName = EventName.TRIGGER_STAY; 
        }
        private void OnTriggerStay(Collider other){
            if (_triggerDetectionMode == TriggerDetectionMode.Layer && other.gameObject.layer == _triggerLayer){
                base.DispatchEvent();
            }

            if (_triggerDetectionMode == TriggerDetectionMode.Tag && other.gameObject.tag == _triggerTag){
                base.DispatchEvent();
            }
        }
    } 
}