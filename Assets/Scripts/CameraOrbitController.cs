using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbitController : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _offset = 10f;
    [SerializeField] private float _rotationSpeed = 10f;

    private Vector2 _inputMouse;
    private float _yaw;
    private float _pitch;
    private float _maxPitch = 70f;
    private float _minPitch = 20f;

    private void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void LateUpdate()
    {
        //Rotar-Mover
        RotateCamera();
        MoveCamera();

        //Mover-Rotar
        this._inputMouse = InputManager.Actions.Player.Mouse.ReadValue<Vector2>();

        this._yaw += this._inputMouse.x * this._rotationSpeed * Time.deltaTime;
        this._pitch -= this._inputMouse.y * this._rotationSpeed * Time.deltaTime; // Invertimos Y
        _pitch = Mathf.Clamp(_pitch, _minPitch, _maxPitch); // Evitamos que la cámara gire completamente hacia arriba o abajo

        Quaternion rotation = Quaternion.Euler(this._pitch, this._yaw, 0);

        Vector3 cameraDirection = rotation * Vector3.forward * _offset;// Quaternion por un vector3 nos devuelve un Vector3
        //Vector3 cameraDirection2 = Vector3.forward * rotation; // Esto no es correcto, no se puede multiplicar un Vector3 por un Quaternion
        this.transform.position = this._target.position - cameraDirection;
        this.transform.LookAt(this._target.position);
    }

    private void RotateCamera()
    {
        this._inputMouse = InputManager.Actions.Player.Mouse.ReadValue<Vector2>();

        this._yaw += this._inputMouse.x * this._rotationSpeed * Time.deltaTime;
        this._pitch -= this._inputMouse.y * this._rotationSpeed * Time.deltaTime; // Invertimos Y
        _pitch = Mathf.Clamp(_pitch, _minPitch, _maxPitch); // Evitamos que la cámara gire completamente hacia arriba o abajo

        Quaternion rotation = Quaternion.Euler(this._pitch, this._yaw, 0);

        this.transform.rotation = rotation;
    }

    private void MoveCamera()
    {
        Vector3 moveCamera = this.transform.forward * this._offset;
        Vector3 newPosition = this._target.position - moveCamera;

        this.transform.position = newPosition;
    }

    
}
