using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitController : MonoBehaviour
{
    [SerializeField] private float _mouseSensibility = 5f;
    private Vector2 _mouseInputPlayer;
    private float _yaw;
    private float _pitch;

    void Awake()
    {
        InputManager.SwitchControlMap(InputManager.ControlMap.Player);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        RotateOrbit();
    }

    private void RotateOrbit()
    {
        this._mouseInputPlayer = InputManager.Actions.Player.Mouse.ReadValue<Vector2>();

        this._yaw += this._mouseInputPlayer.x * Time.deltaTime * _mouseSensibility;// += para que se acumule la rotacion si no se moveria un poco y volveria
        this._pitch += this._mouseInputPlayer.y * Time.deltaTime * _mouseSensibility;// += para que se acumule la rotacion si no se moveria un poco y volveria
        Quaternion rotation = Quaternion.Euler(-_pitch, _yaw, 0);
        this.transform.rotation = rotation;
    }
}
