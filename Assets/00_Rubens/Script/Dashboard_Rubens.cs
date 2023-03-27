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

    List<Object_XNod> _photoElementToInstanciateOnDashboard = new();
    List<NarrativeBloc_XNode> _NarrativeBlocToInstanciateOnDashboard = new();
    List<UnderBloc_XNode> _UnderBlocToInstanciateOnDashboard = new();


    [SerializeField] GameObject prefabElement;

    [Header("Max Elements")]
    [SerializeField] public Transform MaxTopElement;
    [SerializeField] Transform MaxBotElement;
    [SerializeField] Transform MaxRightElement;
    [SerializeField] Transform MaxLeftElement;

    [Header("Camera Limitations")]
    [SerializeField] Transform CameraLimitationTopRight;
    [SerializeField] Transform CameraLimitationBotRight;
    [SerializeField] Transform CameraLimitationTopLeft;
    [SerializeField] Transform CameraLimitationBotLeft;

    Vector3 CameraLimitationCenter;

    [Space (10)]

    [SerializeField] Transform ParentOfElements;

    public List<ElementDashboard> MyElements = new();

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

        //CameraLimitationCenter = CameraLimitationBotLeft.position + (1 / 2) * (-CameraLimitationBotLeft.position + CameraLimitationBotRight.position) + (1 / 2) * (-CameraLimitationBotLeft.position + CameraLimitationTopLeft.position);
         
        CameraLimitationCenter = (CameraLimitationBotLeft.position + CameraLimitationTopRight.position) / 2 ;

        Debug.Log("Les coordonn�es de la limite de cam�ra centre sont" + CameraLimitationCenter);

    }

    private void OnEnable()
    {
        StoryManager.EndOfDay += AddNewElementsOnDashboard;
    }

    private void OnDisable()
    {
        StoryManager.EndOfDay -= AddNewElementsOnDashboard;
    }

    public void SetPictureForNextDay(Object_XNod[] ox)
    {
        List<Object_XNod> oxToListObject = ox.ToList();

        oxToListObject.RemoveAll(k => k == null);

        _photoElementToInstanciateOnDashboard = oxToListObject;
    }

    public void SetNewBlocForNextDay(NarrativeBloc_XNode nBx)
    {
        _NarrativeBlocToInstanciateOnDashboard.Add(nBx);
    }

    public void SetNewUnderBlocForNextDay(UnderBloc_XNode uBx)
    {
        _UnderBlocToInstanciateOnDashboard.Add(uBx);
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
        int index = 0;

        foreach(Object_XNod curentObject in _photoElementToInstanciateOnDashboard)
        {
            float xDashboard = UnityEngine.Random.Range(MaxLeftElement.position.x, MaxRightElement.position.x);
            float yDashboard = UnityEngine.Random.Range(MaxTopElement.position.y, MaxBotElement.position.y);

            ElementDashboard NewDashElement = Instantiate(prefabElement, new Vector3(xDashboard, yDashboard, MaxTopElement.position.z), MaxTopElement.rotation, ParentOfElements).GetComponent<ElementDashboard>();
            NewDashElement.name = ($"Dashboard Element {index} : " + curentObject.NameOfTheObject).RemoveIllegalCharactersFromRubensDesignerMagicTool();
            NewDashElement.Initialize(curentObject);
            index++;

            MyElements.Add(NewDashElement);
        }

        _photoElementToInstanciateOnDashboard.Clear();
    }

    void AddNarrativeBlocOnDashboard()
    {
        foreach(NarrativeBloc_XNode curentObject in _NarrativeBlocToInstanciateOnDashboard)
        {
            float xDashboard = UnityEngine.Random.Range(MaxLeftElement.position.x, MaxRightElement.position.x);
            float yDashboard = UnityEngine.Random.Range(MaxTopElement.position.y, MaxBotElement.position.y);

            ElementDashboard NewDashElement = Instantiate(prefabElement, new Vector3(xDashboard, yDashboard, MaxTopElement.position.z), MaxTopElement.rotation, ParentOfElements).GetComponent<ElementDashboard>();

            NewDashElement.Initialize(curentObject);

            MyElements.Add(NewDashElement);
        }

        _NarrativeBlocToInstanciateOnDashboard.Clear();

    }

    void AddUnderBlocOnDashboard()
    {
        foreach (UnderBloc_XNode curentObject in _UnderBlocToInstanciateOnDashboard) 
        { 
            float xDashboard = UnityEngine.Random.Range(MaxLeftElement.position.x, MaxRightElement.position.x);
            float yDashboard = UnityEngine.Random.Range(MaxTopElement.position.y, MaxBotElement.position.y);

            ElementDashboard NewDashElement = Instantiate(prefabElement, new Vector3(xDashboard, yDashboard, MaxTopElement.position.z), MaxTopElement.rotation, ParentOfElements).GetComponent<ElementDashboard>();

            NewDashElement.Initialize(curentObject);

            MyElements.Add(NewDashElement);
        }

        _UnderBlocToInstanciateOnDashboard.Clear();

    }

    void AddNewElementsOnDashboard()
    {
        AddNarrativeBlocOnDashboard();
        AddUnderBlocOnDashboard();
        AddPictureOnDashboard();
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

        Vector3 ClosestPointOnLineBaseSommet = GetClosestPointOnLine(EmplacementCamera.position, CameraLimitationCenter, NextPosition);

        float MagnitudeMax = (-CameraLimitationCenter + EmplacementCamera.position).magnitude;


        if ((-CameraLimitationCenter + ClosestPointOnLineBaseSommet).magnitude >= MagnitudeMax)
        {
            CameraForDashboard.transform.position = EmplacementCamera.position;

            return;
        }

        if ((-EmplacementCamera.position + ClosestPointOnLineBaseSommet).magnitude >= .95f * MagnitudeMax)
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
