using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Escale.Exyde
{
    [RequireComponent(typeof(Rigidbody))]
    public class FloatingObject : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField][Range(0, 8)] float _underwaterDrag = 3f;
        [SerializeField][Range(0, 8)] float _underwaterAngularDrag = 1f;
        [SerializeField][Range(0, 8)] float _airDrag = 0f;
        [SerializeField][Range(0, 1)] float _airAngularDrag = 0.05f;
        [SerializeField][Range(0, 2000)] float _floatingPower = 15f;

        [SerializeField] Transform[] _floaters;

        public float _waterHeight;

        Rigidbody _rb;
        public bool _isUnderwater;
        int _floatersUnderwater = 0;

        [Header("Waves")]
        [SerializeField] float _waveLength = 1.0f;
        [SerializeField] float _waveSpeed = .2f;
        [SerializeField] float _waveAmplitude = 0.025f;
        
        void Start()
        {
            _rb = GetComponent<Rigidbody>();
        }

        void FixedUpdate()
        {
            ComputeWavesHeight();
            _floatersUnderwater = 0;
              
            for (int i = 0; i < _floaters.Length; i++){
                float difference =  _floaters[i].position.y - _waterHeight;

                if (difference < 0){
                    _rb.AddForceAtPosition(Vector3.up * _floatingPower * Mathf.Abs(difference), _floaters[i].position, ForceMode.Force);
                    _floatersUnderwater++;

                    if (!_isUnderwater){
                        _isUnderwater = true;
                        SwitchState(_isUnderwater);
                    }
                }   
            }

            if (_isUnderwater && _floatersUnderwater == 0){
                _isUnderwater = false;
                SwitchState(_isUnderwater);
            }
        }

        private void ComputeWavesHeight()
        {
            float k = Mathf.PI * 2f / _waveLength;
            float t = (Time.time * _waveSpeed);
            _waterHeight = Mathf.Sin(t * k) * _waveAmplitude;
        }

        void SwitchState(bool isUnderWater){
            if (_isUnderwater){
                _rb.drag = _underwaterDrag;
                _rb.angularDrag = _underwaterAngularDrag;
            } else{
                _rb.drag = _airDrag;
                _rb.angularDrag = _airAngularDrag;
            }
        }
    }
}

