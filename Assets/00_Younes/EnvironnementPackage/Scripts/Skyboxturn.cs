using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skyboxturn : MonoBehaviour
{
    public bool rotateSkybox = true;
    public float _rotateSpeed = 1.2f;
    void Update()
    {
        if (rotateSkybox){
            RenderSettings.skybox.SetFloat("_Rotation", Time.time * _rotateSpeed);
        }
    }
}
