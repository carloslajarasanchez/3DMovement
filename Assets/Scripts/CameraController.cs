using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform _playerTransform;
    [SerializeField] private float _offset = 5;
    [SerializeField] private float _mouseSensibility = 5f;
    private float _limitPitchUp;
    private float _yaw;
    private float _pitch;

    private Vector2 _mouseInputPlayer;
    // Start is called before the first frame update
    void Start()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        this._mouseInputPlayer = InputManager.Actions.Player.Mouse.ReadValue<Vector2>();

        this._yaw += this._mouseInputPlayer.x * Time.deltaTime * _mouseSensibility;// += para que se acumule la rotacion si no se moveria un poco y volveria
        this._pitch += this._mouseInputPlayer.y * Time.deltaTime * _mouseSensibility;// += para que se acumule la rotacion si no se moveria un poco y volveria
        Quaternion rotation = Quaternion.Euler(-_pitch, _yaw, 0);
        this.transform.rotation = rotation;

        transform.position = _playerTransform.position - transform.forward * _offset;
        //transform.LookAt(_playerTransform);

    }
}
