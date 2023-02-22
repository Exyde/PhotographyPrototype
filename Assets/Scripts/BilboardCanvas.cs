using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BilboardCanvas : MonoBehaviour
{

    Camera _cam;
    void Start()
    {
        _cam = Camera.main;
    }

    void Update()
    {
        transform.rotation = _cam.transform.rotation;
    }
}
