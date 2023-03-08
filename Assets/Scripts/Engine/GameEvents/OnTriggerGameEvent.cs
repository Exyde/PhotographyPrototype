using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace Core.GameEvents{
    [RequireComponent(typeof(BoxCollider))]
    public class OnTriggerGameEvent : GameEvent { //@TODO : Splits in 3 Distinct classes Enter, StayTimer, Exit

        public enum TriggerDetectionMode {Layer, Tag};
        public enum TriggerMode {Enter, Stay, Exit};

        private Collider coll;

        [Header("Settings")]
        [SerializeField] TriggerDetectionMode _triggerDetectionMode = TriggerDetectionMode.Layer;
        [SerializeField] TriggerMode _triggerMode = TriggerMode.Enter;
        [SerializeField] LayerMask _triggerLayer;
        [SerializeField] string _triggerTag;
 
        void Reset(){
            coll = GetComponent<Collider>();
            if (coll == null){
                coll = gameObject.AddComponent<BoxCollider>();
            }
            coll.isTrigger = true;
        }

        private void OnTriggerEnter(Collider other){
            if (_triggerMode != TriggerMode.Enter) return;
            if (_triggerDetectionMode == TriggerDetectionMode.Layer && other.gameObject.layer == _triggerLayer){
                base.DispatchEvent();
            }

            if (_triggerDetectionMode == TriggerDetectionMode.Tag && other.gameObject.tag == _triggerTag){
                base.DispatchEvent();
            }
        }

        private void OnTriggerStay(Collider other){
            if (_triggerMode != TriggerMode.Stay) return;
            if (_triggerDetectionMode == TriggerDetectionMode.Layer && other.gameObject.layer == _triggerLayer){
                base.DispatchEvent();
            }

            if (_triggerDetectionMode == TriggerDetectionMode.Tag && other.gameObject.tag == _triggerTag){
                base.DispatchEvent();
            }
        }

        private void OnTriggerExit(Collider other){
            if (_triggerMode != TriggerMode.Exit) return;
            if (_triggerDetectionMode == TriggerDetectionMode.Layer && other.gameObject.layer == _triggerLayer){
                base.DispatchEvent();
            }

            if (_triggerDetectionMode == TriggerDetectionMode.Tag && other.gameObject.tag == _triggerTag){
                base.DispatchEvent();
            }
        }
    }
}