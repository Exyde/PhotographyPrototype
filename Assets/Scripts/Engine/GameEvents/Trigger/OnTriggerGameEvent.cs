using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace Core.GameEvents{
    [RequireComponent(typeof(BoxCollider))]
    public class OnTriggerGameEvent : GameEvent { //@TODO : Splits in 3 Distinct classes Enter, StayTimer, Exit

        protected enum TriggerDetectionMode {Layer, Tag};
        protected enum TriggerMode {Enter, Stay, Exit};

        private Collider coll;

        [Header("Settings")]
        [SerializeField] protected TriggerDetectionMode _triggerDetectionMode = TriggerDetectionMode.Layer;
        [SerializeField] protected TriggerMode _triggerMode = TriggerMode.Enter;
        [SerializeField] protected LayerMask _triggerLayer;
        [SerializeField] protected string _triggerTag;
 
        void Reset(){
            coll = GetComponent<Collider>();
            if (coll == null){
                coll = gameObject.AddComponent<BoxCollider>();
            }
            coll.isTrigger = true;
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