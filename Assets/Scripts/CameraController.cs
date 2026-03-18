using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float _offset = 5f;
    [SerializeField] private float _mouseSensibility = 5f;

    [Header("Pitch Limits")]
    [SerializeField] private float _limitPitchUp = 60f;
    [SerializeField] private float _limitPitchDown = 15f;

    private float _yaw;
    private float _pitch;
    private Transform _playerTransform;

    void Start()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        CameraRotation();
        UpdateCameraPosition();
    }

    private void CameraRotation()
    {
        Vector2 mouseInput = InputManager.Actions.Player.Mouse.ReadValue<Vector2>();

        _yaw += mouseInput.x * _mouseSensibility * Time.deltaTime;
        _pitch -= mouseInput.y * _mouseSensibility * Time.deltaTime; // Invertimos Y
        _pitch = Mathf.Clamp(_pitch, _limitPitchDown, _limitPitchUp);

        transform.rotation = Quaternion.Euler(_pitch, _yaw, 0);
    }

    private void UpdateCameraPosition()
    {
        transform.position = _playerTransform.position - transform.forward * _offset;
    }
}
