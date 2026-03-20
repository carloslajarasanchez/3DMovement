using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private float _walkThreshold = 0.1f;
    private Animator _animator;
    private CharacterController _characterController;
    private Character3DController _character3DController;
    // Start is called before the first frame update
    void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _character3DController = GetComponent<Character3DController>();
        _animator = GetComponent<Animator>();
        //Main.CustomEvents.OnJumped?.AddListener(HandleJumpAnimation);
    }

    // Update is called once per frame
    void Update()
    {
        HandleWalkAnimation();
        HandleJumpAnimation();
        // Debug.Log($"Velocidad: {_characterController.velocity}");
    }

    private void HandleWalkAnimation()
    {
        Vector2 input = InputManager.Actions.Player.Move.ReadValue<Vector2>();
        _animator.SetFloat("Velocity", input.magnitude);
    }

    private void HandleJumpAnimation()
    {
        bool isGrounded = _character3DController.IsGrounded;

        // Solo llamar SetBool si cambia el valor
        if (_animator.GetBool("IsGrounded") != isGrounded)
            _animator.SetBool("IsGrounded", isGrounded);
    }

    private void OnDestroy()
    {
        //Main.CustomEvents.OnJumped?.RemoveListener(HandleJumpAnimation);
    }
}
