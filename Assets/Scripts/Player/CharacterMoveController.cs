using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class CharacterMoveController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _rotationSpeed = 5f;

    private Vector3 playerVelocity;
    private float gravityValue = -9.81f;
    private bool groundedPlayer;
    private CharacterController _characterController;
    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }
    void Start()
    {
        InputManager.SwitchControlMap(InputManager.ControlMap.Player);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Gravedad
        groundedPlayer = _characterController.isGrounded;

        if (groundedPlayer)
        {
            // Slight downward velocity to keep grounded stable
            if (playerVelocity.y < -2f)
                playerVelocity.y = -2f;
        }

        playerVelocity.y += gravityValue * Time.deltaTime;

        Vector2 inputPlayer = InputManager.Actions.Player.Move.ReadValue<Vector2>();

        Vector3 move = Vector3.zero;
        if (inputPlayer.magnitude > .1f)
        {

            // Direccion del movimiento
            var cameraForward = Camera.main.transform.forward;
            var cameraRight = Camera.main.transform.right;// ver como añado el cameraRight
            var playerDirection = new Vector3(cameraForward.x, 0, cameraForward.z);

            playerDirection = Vector3.ClampMagnitude(playerDirection, 1f);
            move = playerDirection * inputPlayer.y + cameraRight * inputPlayer.x;

            // Rotacion del personaje
            Quaternion rotation = Quaternion.LookRotation(playerDirection, Vector3.up);
            this.transform.rotation = rotation;

        }

        move = move * _moveSpeed + Vector3.up * playerVelocity.y;
        _characterController.Move(move * Time.deltaTime);
    }
}
