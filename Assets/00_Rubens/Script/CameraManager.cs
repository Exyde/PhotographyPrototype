using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraManager : MonoBehaviour
{
    public Action<bool> OnCameraPlayerSetActive;

    [HideInInspector]
    public Camera CameraPlayer;

    [HideInInspector]
    public Camera CameraDashboard;

    [HideInInspector]
    public PlayerController PC;
         
    [HideInInspector]
    public Transform EmplacementCameraDashboard;

    public static CameraManager CM;

    IEnumerator _coroutine;

    void Awake()
    {
        if (CM == null)
        {
            CM = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void TransitionToDashBoard(float duration)
    {
        if(CameraPlayer == null || CameraDashboard == null || EmplacementCameraDashboard == null)
        {
            Debug.LogWarning("Une des Camera ou emplacement de camera n'est pas corectement assigne au CameraManager. Normalement Dashboard et PlayerCOntroller l'assigne a leur Start().");
            return;
        }

        PC.TogglePlayerCameraMovement(false);

        

        CameraDashboard.transform.position = CameraPlayer.transform.position;
        CameraDashboard.transform.rotation = CameraPlayer.transform.rotation;

        CameraDashboard.gameObject.SetActive(true);
        CameraPlayer.gameObject.SetActive(false);

        PC.gameObject.transform.localEulerAngles =
            new Vector3(
                PC.gameObject.transform.localEulerAngles.x,
                EmplacementCameraDashboard.transform.eulerAngles.y,
                PC.gameObject.transform.localEulerAngles.z);

        //PC.gameObject.transform.eulerAngles.Set(PC.gameObject.transform.eulerAngles.x, CameraDashboard.transform.eulerAngles.y, PC.gameObject.transform.eulerAngles.z); 



        PC.gameObject.transform.rotation.eulerAngles.Set(0, 0, 0);

        _coroutine = IE_LerpCameraPositionAndRotation(CameraDashboard.transform, EmplacementCameraDashboard, duration);
        StartCoroutine(_coroutine);

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void TransitionToFPS(float duration)
    {
        if (CameraPlayer == null || CameraDashboard == null || EmplacementCameraDashboard == null)
        {
            Debug.LogWarning("Une des Camera ou emplacement de camera n'est pas corectement assigne au CameraManager. Normalement Dashboard et PlayerCOntroller l'assigne a leur Start().");
            return;
        }

        _coroutine = IE_LerpCameraPositionAndRotation(CameraDashboard.transform, CameraPlayer.transform, duration);
        StartCoroutine(_coroutine);


        _coroutine = IE_SetActiveCameraWithDelay(false, true, duration);
        StartCoroutine(_coroutine);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    IEnumerator IE_LerpCameraPositionAndRotation(Transform camera, Transform to, float duration)
    {
        float tt = 0;

        Vector3 initialPosition = camera.position;

        Quaternion initialRotation = camera.rotation;


        while (tt <= duration)
        {
            camera.position = Vector3.Lerp(initialPosition, to.position, tt / duration);

            camera.rotation = Quaternion.Lerp(initialRotation, to.rotation, tt / duration);

            tt += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }

        camera.position = to.position;
        camera.rotation = to.rotation;

    }

    IEnumerator IE_SetActiveCameraWithDelay(bool cameraDashboard, bool cameraPlayer, float duration)
    {
        yield return new WaitForSeconds(duration);

        CameraDashboard.gameObject.SetActive(cameraDashboard);
        CameraPlayer.gameObject.SetActive(cameraPlayer);

        PC.TogglePlayerCameraMovement(true);
    }
}
