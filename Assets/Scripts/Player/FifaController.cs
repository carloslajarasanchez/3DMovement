using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FifaController : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _rotateSpeed = 100f;
    private Vector2 _inputPlayer;

    // Start is called before the first frame update
    void Awake()
    {
        InputManager.SwitchControlMap(InputManager.ControlMap.Player);
    }

    // Update is called once per frame
    void Update()
    {
        MoveFifa();
    }

    private void MoveFifa()
    {
        this._inputPlayer = InputManager.Actions.Player.Move.ReadValue<Vector2>();

        // Movement
        var direction = new Vector3(this._inputPlayer.x, 0, this._inputPlayer.y);

        if (direction.magnitude > 0)
        {
            Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, _rotateSpeed * Time.deltaTime);
            transform.position += transform.forward * _speed * Time.deltaTime;
        }
    }
}
