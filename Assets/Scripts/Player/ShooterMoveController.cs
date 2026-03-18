using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterMoveController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _rotateSpeed = 100f;

    private Camera _mainCamera;
    private Vector3 _cameraForward;

    private void Awake()
    {
        InputManager.SwitchControlMap(InputManager.ControlMap.Player);
        _mainCamera = Camera.main;
    }
    private void Update()
    {
        Vector2 input = InputManager.Actions.Player.Move.ReadValue<Vector2>();
        if (input == Vector2.zero) return;

        Vector3 moveDirection = GetCameraRelativeDirection(input);
        RotateTowards(moveDirection);
        Move(moveDirection);
    }

    private Vector3 GetCameraRelativeDirection(Vector2 input)
    {
        // Anulamos el componente Y para que el movimiento sea horizontal
        _cameraForward = Vector3.Scale(_mainCamera.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 camRight = Vector3.Scale(_mainCamera.transform.right, new Vector3(1, 0, 1)).normalized;

        return _cameraForward * input.y + camRight * input.x;
    }

    private void RotateTowards(Vector3 direction)
    {
        Quaternion targetRotation = Quaternion.LookRotation(_cameraForward, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, _rotateSpeed * Time.deltaTime);
    }

    private void Move(Vector3 direction)
    {
        transform.position += direction * _speed * Time.deltaTime;
    }
}
