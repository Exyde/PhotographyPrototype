using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleOscillator : MonoBehaviour
{
    [SerializeField] float _amplitude = 1.0f;
    [SerializeField] float _frequency = .2f;
    void Update()
    {
        Vector3 previousScale = transform.localScale;
        previousScale.x = Mathf.Sin(Time.time * _frequency) * _amplitude;
        transform.localScale = previousScale;
    }
}
