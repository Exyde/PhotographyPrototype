using UnityEngine;

namespace Core.GameEvents{
    public class OnTriggerExitEvent : OnTriggerGameEvent{
        private void OnTriggerExit(Collider other){
            if (_triggerDetectionMode == TriggerDetectionMode.Layer && other.gameObject.layer == _triggerLayer){
                base.DispatchEvent();
            }

            if (_triggerDetectionMode == TriggerDetectionMode.Tag && other.gameObject.tag == _triggerTag){
                base.DispatchEvent();
            }
        }
    }
}
