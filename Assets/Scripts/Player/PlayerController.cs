using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    enum CameraState{
        FPS, Blackboard
    }

    [SerializeField] KeyCode _cameraToggleKeyCode = KeyCode.T;

    #region Fields
    // Public Fields
    [Header("Camera Stuffs")]
    [SerializeField] CameraState _camState = CameraState.FPS;
    public Camera _mainCamera;
    public Transform _blackboardCameraHolder;
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

    [Header("Blackboard View Settings")] //@TODO : AddBlackboard Interaction
    [SerializeField] float _cursorSpeed = 0f;

    //Private Fields
    CharacterController _characterController;
    Vector3 _moveDirection = Vector3.zero;
    Rigidbody _rb;
    float _rotationX = 0;

    public bool _canMove = true;
    public bool _useLegacyCamera = false;

    #endregion
   
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
        if (Input.GetKeyDown(_cameraToggleKeyCode)) ToggleCamera();
        if ( _camState == CameraState.FPS) HandleMovement();
        else if (_camState == CameraState.Blackboard) HandleBlackboardInteraction();
    }
    #endregion

    #region Methods & Handlers
    void HandleMovement(){

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = _canMove ? (isRunning ? _runningSpeed : _walkingSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = _canMove ? (isRunning ? _runningSpeed : _walkingSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = _moveDirection.y;
        _moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && _canMove)
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

        if (_canMove)
        {
            _rotationX += -Input.GetAxis("Mouse Y") * _lookSpeed;
            _rotationX = Mathf.Clamp(_rotationX, -_lookXLimit, _lookXLimit);

            if (_useLegacyCamera) _mainCamera.transform.localRotation = Quaternion.Euler(_rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * _lookSpeed, 0);
        }
    }

    void HandleBlackboardInteraction(){
        Debug.Log("Blackboard stuffs !");
        //@TODO : Disable Rendering while in Blacboard => Toggle ? 
    }
    #endregion

    #region Camera Switch and Lerping
    void ToggleCamera(){
        if(_camState == CameraState.FPS) SetBlackboardCamera(false);
        else if (_camState == CameraState.Blackboard) SetFPSCamera(true);
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
    void SetBlackboardCamera(bool lerp){

        SetCamera(CameraState.Blackboard, _blackboardCameraHolder, _blackboardCameraHolder.position, lerp);
        _mainCamera.transform.LookAt(_blackboardCameraHolder.parent.transform);
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