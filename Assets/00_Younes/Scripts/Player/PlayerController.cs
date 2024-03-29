using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour 
{
    #region Fields
    // Public Fields
    [Header("Camera Stuffs")]
    public Camera _mainCamera;

    [Header("FPS Settings")]
    public float _walkingSpeed = 7.5f;
    public float _runningSpeed = 11.5f;
    public float _jumpSpeed = 8.0f;
    public float _gravity = 20.0f;
    public float _lookSpeed = 2.0f;
    public float _lookXLimit = 45.0f;

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

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if(CameraManager.CM == null)
        {
            Debug.LogWarning("Attention le Camera Manager n'est pas dans la scene !!!!");
            return;
        }

        CameraManager.CM.PC = this;
        CameraManager.CM.CameraPlayer = _mainCamera;
    }

    void Update()
    {
        HandleMovement();
        HandleCameraMovement();
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
    #endregion
}