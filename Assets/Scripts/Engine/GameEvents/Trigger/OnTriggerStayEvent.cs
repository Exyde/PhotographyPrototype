using UnityEngine; //@TODO : Add Stay timing behavior here

namespace Core.GameEvents{
    public class OnTriggerStayEvent : OnTriggerGameEvent{
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