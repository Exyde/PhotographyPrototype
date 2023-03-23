using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.EventSystems;

public class Dashboard_Rubens : MonoBehaviour, IScrollHandler
{
    #region variables

    [HideInInspector]
    public float offSetValueForpicture;

    public float offSetValueForpictureToPut = .43f;

    public Transform EmplacementCamera;

    public Camera CameraForDashboard;

    [SerializeField] float _transitionCameraDuration = 1f;

    public static Dashboard_Rubens DB;

    [SerializeField] List<Object_XNod> _objectToInstanciateOnDashboard;

    [SerializeField] GameObject prefabElement;

    [SerializeField] Transform MaxTopElement;
    [SerializeField] Transform MaxBotElement;
    [SerializeField] Transform MaxRightElement;
    [SerializeField] Transform MaxLeftElement;

    [SerializeField] Transform CameraLimitationTopRight;
    [SerializeField] Transform CameraLimitationBotRight;
    [SerializeField] Transform CameraLimitationTopLeft;
    [SerializeField] Transform CameraLimitationBotLeft;
    Vector3 CameraLimitationCenter;

    [SerializeField] Transform ParentOfElements;

    #endregion

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

        offSetValueForpicture = transform.position.z + offSetValueForpictureToPut;
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

        CameraLimitationCenter = CameraLimitationBotLeft.position + (1 / 2) *(-CameraLimitationBotLeft.position + CameraLimitationBotRight.position) + (1 / 2) * (-CameraLimitationBotLeft.position + CameraLimitationTopLeft.position);

    }

    private void OnEnable()
    {
        StoryManager.EndOfDay += AddPictureOnDashboard;
    }

    private void OnDisable()
    {
        StoryManager.EndOfDay -= AddPictureOnDashboard;
    }

    public void SetPictureForNextDay(Object_XNod[] ox)
    {
        _objectToInstanciateOnDashboard = ox.ToList();
    }

    public void ActivateDashboardMode()
    {
        CameraManager.CM.TransitionToDashBoard(_transitionCameraDuration);
        HUDManager._instance.SetActivCrossAir(false);
    }

    public void DesactivateDashboardMode()
    {
        CameraManager.CM.TransitionToFPS(_transitionCameraDuration);
        HUDManager._instance.SetActivCrossAir(true);

    }

    [ContextMenu("AddPictureOnDashBoard")]
    void AddPictureOnDashboard()
    {
        foreach(Object_XNod curentObject in _objectToInstanciateOnDashboard)
        {
            float xDashboard = UnityEngine.Random.Range(MaxLeftElement.position.x, MaxRightElement.position.x);
            float yDashboard = UnityEngine.Random.Range(MaxTopElement.position.y, MaxBotElement.position.y);

            Instantiate(prefabElement, new Vector3(xDashboard, yDashboard, MaxTopElement.position.z), MaxTopElement.rotation, ParentOfElements).
                GetComponent<ElementDashboard>().
                Instanciate(curentObject);
        }

        _objectToInstanciateOnDashboard.Clear();
    }

    public void OnScroll(PointerEventData eventData)
    {
        int SensOfZoom = (int)eventData.scrollDelta.y;

        Vector3 DirectionOfZoom = Vector3.zero;

        if (SensOfZoom < 0)
        {
            DirectionOfZoom = EmplacementCamera.position - CameraForDashboard.transform.position;
        }
        else 
        {
            DirectionOfZoom = eventData.pointerCurrentRaycast.worldPosition - CameraForDashboard.transform.position;
        }

        DirectionOfZoom = DirectionOfZoom.normalized;

        Vector3 NextPosition = CameraForDashboard.transform.position + DirectionOfZoom * 0.1f;

        Vector3 ClosestPointOnLineBaseSommet = GetClosestPointOnLine(-EmplacementCamera.position, CameraLimitationCenter, NextPosition);

        if ((-CameraLimitationCenter + ClosestPointOnLineBaseSommet).magnitude >= (-CameraLimitationCenter + EmplacementCamera.position).magnitude)
        {
            CameraForDashboard.transform.position = EmplacementCamera.position;
            return;
        }

        if ((-EmplacementCamera.position + ClosestPointOnLineBaseSommet).magnitude >= .9f * (-EmplacementCamera.position + CameraLimitationCenter).magnitude)
        {
            return;
        }

        CameraForDashboard.transform.position = NextPosition;

    }

    public Vector3 GetClosestPointOnLine(Vector3 A, Vector3 B, Vector3 C)
    //Merci ChatGpt => "trouver le point sur une ligne AB le plus proche d'un point C dans un espace 3D"
    {
        Vector3 AB = B - A;
        Vector3 AC = C - A;
        float ab2 = Vector3.Dot(AB, AB);
        float abac = Vector3.Dot(AB, AC);
        float t = abac / ab2;
        return A + AB * t;
    }

}
