using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Dashboard_Rubens : MonoBehaviour
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
    }

    public void DesactivateDashboardMode()
    {
        CameraManager.CM.TransitionToFPS(_transitionCameraDuration);
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

}
