using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCameraFowardController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
     private void Awake()
    {
        InputManager.SwitchControlMap(InputManager.ControlMap.Player);
    }

    private void Update()
    {
        var inputPlayer = InputManager.Actions.Player.Move.ReadValue<Vector2>();

        if (inputPlayer.magnitude < .1f) return;
        
        var cameraDirection = Camera.main.transform.forward;
        var playerDirection = new Vector3(cameraDirection.x, 0, cameraDirection.z);

        Quaternion rotation = Quaternion. LookRotation(playerDirection, Vector3.up);
        this.transform.rotation = rotation;

        this.transform.position += this.transform.forward * inputPlayer.y * _moveSpeed * Time.deltaTime;
        this.transform.position += this.transform.right * inputPlayer.x * _moveSpeed * Time.deltaTime;
    }
}
