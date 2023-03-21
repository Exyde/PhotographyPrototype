using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dashboard_Rubens : MonoBehaviour
{
    public float offSetValueForpicture = -6.03f;

    public Transform EmplacementCamera;

    public Camera CameraForDashboard;

    [SerializeField] float _transitionCameraDuration = 1f;

    public static Dashboard_Rubens DB;


    void Awake()
    {
        if (DB == null)
        {
            DB = this;
            //DontDestroyOnLoad(this.gameObject);

        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        if (CameraManager.CM == null)
        {
            Debug.LogWarning("Attention le Camera Manager n'est pas dans la scene");
            return;
        }

        CameraManager.CM.CameraDashboard = CameraForDashboard;
        CameraManager.CM.EmplacementCameraDashboard = EmplacementCamera;
    }

    public void SetPictureForNextDay(List<Object_XNod> ox)
    {

    }

    public void ActivateDashboardMode()
    {
        CameraManager.CM.TransitionToDashBoard(_transitionCameraDuration);
    }

    public void DesactivateDashboardMode()
    {
        CameraManager.CM.TransitionToFPS(_transitionCameraDuration);
    }

}
