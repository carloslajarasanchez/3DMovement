using UnityEngine;
using UnityEngine.InputSystem;

public class Character3DController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _rotationSpeed = 10f;
    [SerializeField] private float _jumpHeight = 5f;

    private const float Gravity = -9.81f;

    private float _verticalVelocity;
    private bool _isGrounded;
    private CharacterController _characterController;
    private Transform _cameraTransform;

    public bool IsGrounded { get { return _isGrounded; } private set { _isGrounded = value; } }

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _cameraTransform = Camera.main.transform;
    }

    private void Start()
    {
        InputManager.SwitchControlMap(InputManager.ControlMap.Player);
        InputManager.Actions.Player.Jump.performed += Jump;
    }

    private void FixedUpdate()
    {
        HandleGravity();
        HandleMovement();
        CheckGround();
    }

    private void HandleGravity()
    {
        if (_isGrounded)
        {
            if (_verticalVelocity < 0f)
                _verticalVelocity = -2f;
        }
        else
        {
            _verticalVelocity += Gravity * Time.deltaTime;
        }
    }

    private void HandleMovement()
    {
        Vector2 input = InputManager.Actions.Player.Move.ReadValue<Vector2>();

        Vector3 moveDirection = Vector3.zero;

        if (input.sqrMagnitude > 0.01f)
        {
            // Proyectar ejes de la cámara sobre el plano horizontal
            Vector3 camForward = Vector3.ProjectOnPlane(_cameraTransform.forward, Vector3.up).normalized;
            Vector3 camRight = Vector3.ProjectOnPlane(_cameraTransform.right, Vector3.up).normalized;

            // Dirección de movimiento real en función del input
            moveDirection = (camForward * input.y + camRight * input.x).normalized;

            // Rotar suavemente hacia donde se mueve
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                _rotationSpeed * Time.deltaTime
            );
        }

        // Combinar movimiento horizontal + velocidad vertical
        Vector3 velocity = moveDirection * _moveSpeed + Vector3.up * _verticalVelocity;
        _characterController.Move(velocity * Time.deltaTime);
    }

    private void Jump(InputAction.CallbackContext ctx)
    {
        if (_isGrounded)
        {
            _isGrounded = false;
            Main.CustomEvents.OnJumped?.Invoke();
            _verticalVelocity = Mathf.Sqrt(_jumpHeight * -2f * Gravity);
        }
    }

    private void CheckGround()
    {
        // Usar una distancia mayor y desde el centro del CharacterController
        float rayDistance = (_characterController.height / 2f) + 0.2f;
        Vector3 rayOrigin = transform.position + Vector3.up * (_characterController.height / 2f);

        _isGrounded = Physics.Raycast(rayOrigin, Vector3.down, rayDistance);
    }

    private void OnDestroy()
    {
        InputManager.Actions.Player.Jump.performed -= Jump;
    }
}
