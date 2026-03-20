using UnityEngine;

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

        Main.CustomEvents.OnAttackCombo?.AddListener(HandleAttackAnimation);
        Main.CustomEvents.OnComboReset?.AddListener(HandleComboResetAnimation);
        Main.CustomEvents.OnPlayerHit?.AddListener(HandleHitAnimation);
    }

    // Update is called once per frame
    void Update()
    {
        HandleWalkAnimation();
        HandleJumpAnimation();
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

    private void HandleAttackAnimation(int comboCount)
    {
        _animator.SetInteger("ComboCount", comboCount);
        _animator.SetBool("IsAttacking", true);
    }

    private void HandleComboResetAnimation()
    {
        _animator.SetBool("IsAttacking", false);
        _animator.SetInteger("ComboCount", 0);
    }

    private void HandleHitAnimation(float damage)
    {
        _animator.SetTrigger("Hit");
    }

    private void OnDestroy()
    {
        Main.CustomEvents.OnAttackCombo?.RemoveListener(HandleAttackAnimation);
        Main.CustomEvents.OnComboReset?.RemoveListener(HandleComboResetAnimation);
        Main.CustomEvents.OnPlayerHit?.RemoveListener(HandleHitAnimation);
    }
}

