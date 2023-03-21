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
        if (_cam == null) return;
        transform.rotation = _cam.transform.rotation;
    }
}
