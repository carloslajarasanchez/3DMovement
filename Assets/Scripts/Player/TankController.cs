using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _mouseSensibility = 5f;

    private float _yaw;
    private Vector2 _inputPlayer;
    private Vector2 _mouseInputPlayer;
    // Start is called before the first frame update
    void Awake()
    {
        InputManager.SwitchControlMap(InputManager.ControlMap.Player);
    }

    // Update is called once per frame
    void Update()
    {      
        MoveTank();
        RotateTank();
    }

    private void MoveTank()
    {
        this._inputPlayer = InputManager.Actions.Player.Move.ReadValue<Vector2>();

        float moveFordward = this._inputPlayer.y * _speed * Time.deltaTime;
        this.transform.position += this.transform.forward * moveFordward;
    }

    private void RotateTank()
    {
        this._mouseInputPlayer = InputManager.Actions.Player.Mouse.ReadValue<Vector2>();

        // Rotation Y
        this._yaw += this._mouseInputPlayer.x * Time.deltaTime * _mouseSensibility;// += para que se acumule la rotacion si no se moveria un poco y volveria
        Quaternion rotation = Quaternion.Euler(0, _yaw, 0);
        this.transform.rotation = rotation;
    }
}
