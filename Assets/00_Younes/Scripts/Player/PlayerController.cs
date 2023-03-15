using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//@TODO :
// - Refactor with GameInputs

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour 
{
    enum CameraState{
        FPS, Dashboard
    }

    #region Fields
    // Public Fields
    [Header("Camera Stuffs")]
    [SerializeField] CameraState _camState = CameraState.FPS;
    public Camera _mainCamera;
    public Transform _dashboardCameraHolder;
    public Transform _fpsCameraHolder;
    public Vector3 _fpsCameraPosition;

    [Header("FPS Settings")]

    public float _moveSpeed = 10f;
    public float _walkingSpeed = 7.5f;
    public float _runningSpeed = 11.5f;
    public float _jumpSpeed = 8.0f;
    public float _gravity = 20.0f;
    public float _lookSpeed = 2.0f;
    public float _lookXLimit = 45.0f;

    //[Header("Dashboard View Settings")] //@TODO : Add Dashboard Interaction System
    //[SerializeField] float _cursorSpeed = 0f;

    //Private Fields
    CharacterController _characterController;
    Vector3 _moveDirection = Vector3.zero;
    Rigidbody _rb;
    float _rotationX = 0;

    public bool _useLegacyCamera = false;
    public bool _canMove = true;
    public bool _canMoveCamera = true;

    #endregion
   
    public void TogglePlayerMovement(bool state) => _canMove = state;
    public void TogglePlayerCameraMovement(bool state) => _canMoveCamera = state;

   
    #region Unity Callbacks
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _rb = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;//@TODO : ToggleCursorMethod ?
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(GameInputs.CameraModeToggleKeyCode)) ToggleCamera();

        if ( _camState == CameraState.FPS){
            HandleMovement();
            HandleCameraMovement();
        } 
        else if (_camState == CameraState.Dashboard){
            HandleDashboardInteraction();
        } 
    }
    #endregion

    #region Methods & Handlers
    void HandleMovement(){

        if (!_canMove) return;

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = (isRunning ? _runningSpeed : _walkingSpeed) * Input.GetAxis("Vertical");
        float curSpeedY = (isRunning ? _runningSpeed : _walkingSpeed) * Input.GetAxis("Horizontal");
        float movementDirectionY = _moveDirection.y;
        _moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump"))
        {
            _moveDirection.y = _jumpSpeed;
        }
        else
        {
            _moveDirection.y = movementDirectionY;
        }

        if (!_characterController.isGrounded)
        {
            _moveDirection.y -= _gravity * Time.deltaTime;
        }

        _characterController.Move(_moveDirection * Time.deltaTime);
    }

    void HandleCameraMovement(){
        if (!_canMoveCamera) return;
        _rotationX += -Input.GetAxis("Mouse Y") * _lookSpeed;
        _rotationX = Mathf.Clamp(_rotationX, -_lookXLimit, _lookXLimit);

        if (_useLegacyCamera) _mainCamera.transform.localRotation = Quaternion.Euler(_rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * _lookSpeed, 0);
    }

    void HandleDashboardInteraction(){
        //Logger.Log("Dashboard stuffs !");
        //@TODO : Disable Player Rendering while in Blacboard => Toggle ? 
        // => Use a dashboard controller
    }
    #endregion

    #region Camera Switch and Lerping
    void ToggleCamera(){
        if(_camState == CameraState.FPS && _fpsCameraHolder != null) SetDashboardCamera(false);
        else if (_camState == CameraState.Dashboard) SetFPSCamera(true);
    }

    //@TODO : Must Become a Coroutine ?
    void SetCamera(CameraState state, Transform parent, Vector3 targetPos = default(Vector3), bool lerpPosition = false, float transitionDuration = 1f){
        _camState = state;
        _mainCamera.transform.parent = parent;

        if (lerpPosition)
            StartCoroutine(LerpCameraPosition(targetPos, transitionDuration));
        else
            _mainCamera.transform.position = targetPos;
    }
    void SetFPSCamera(bool lerp) => SetCamera(CameraState.FPS, _fpsCameraHolder, _fpsCameraHolder.position, lerp, 2f);

    void SetDashboardCamera(bool lerp){

        SetCamera(CameraState.Dashboard, _dashboardCameraHolder, _dashboardCameraHolder.position, lerp);
        _mainCamera.transform.LookAt(_dashboardCameraHolder.parent.transform);
    }

    IEnumerator LerpCameraPosition(Vector3 to, float t){
        float tt = 0;
        while (Vector3.Distance(_mainCamera.transform.position, to) > .2f){
            _mainCamera.transform.position = Vector3.Lerp(_mainCamera.transform.position, to, tt/t);
            tt += Time.deltaTime;
            yield return null;
        }
    }
    #endregion
}