using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _mouseSensibility = 5f;
    [SerializeField]private float _rotateSpeed = 100f;
    private Vector2 _inputPlayer;
    private Vector2 _mouseInputPlayer;
    private float _yaw;
    private float _pitch;
    private float _rotate;


    private void Awake()
    {
        InputManager.SwitchControlMap(InputManager.ControlMap.Player);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        this._inputPlayer = InputManager.Actions.Player.Move.ReadValue<Vector2>();   
        this._mouseInputPlayer = InputManager.Actions.Player.Mouse.ReadValue<Vector2>();   

        // Movement
        var direction = new Vector3(this._inputPlayer.x, 0, this._inputPlayer.y);

        /*if(direction.magnitude > 0)
        {
            Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, _rotateSpeed * Time.deltaTime);
            transform.position += transform.forward * _speed * Time.deltaTime;
        }*/


        /*float moveFordward = this._inputPlayer.y * _speed * Time.deltaTime;
        this.transform.position += this.transform.forward * moveFordward;
        Debug.Log("Move: " + this.transform.forward.magnitude);

        _rotate += this._inputPlayer.x * _rotateSpeed * Time.deltaTime;
        var Rotate = Quaternion.Euler(0, _rotate, 0);
        //this.transform.rotation = Rotate;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Rotate, _rotateSpeed * Time.deltaTime);
        transform.position += transform.forward * moveFordward;*/

        // Orbitar Rotation
        this._yaw += this._mouseInputPlayer.x * Time.deltaTime * _mouseSensibility;// += para que se acumule la rotacion si no se moveria un poco y volveria
        this._pitch += this._mouseInputPlayer.y * Time.deltaTime * _mouseSensibility;// += para que se acumule la rotacion si no se moveria un poco y volveria
        Quaternion rotation = Quaternion.Euler(-_pitch, _yaw, 0);
        this.transform.rotation = rotation;

        // Rotation Y
        /*this._yaw += this._mouseInputPlayer.x * Time.deltaTime * _mouseSensibility;// += para que se acumule la rotacion si no se moveria un poco y volveria
        Quaternion rotation = Quaternion.Euler(0, _yaw, 0);
        this.transform.rotation = rotation;*/
    }
}
