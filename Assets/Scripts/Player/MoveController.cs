using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _mouseSensibility = 5f;
    private Vector2 _inputPlayer;
    private Vector2 _mouseInputPlayer;


    private void Awake()
    {
        InputManager.SwitchControlMap(InputManager.ControlMap.Player);
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        this._inputPlayer = InputManager.Actions.Player.Move.ReadValue<Vector2>();   
        this._mouseInputPlayer = InputManager.Actions.Player.Mouse.ReadValue<Vector2>();   

        Vector3 newPosition = new Vector3(this._inputPlayer.x, 0, this._inputPlayer.y) * _speed * Time.deltaTime ;
        this.transform.position += newPosition;

        this.transform.rotation = Quaternion.Euler(0, this._mouseInputPlayer.x * _mouseSensibility, 0) ;
    }
}
