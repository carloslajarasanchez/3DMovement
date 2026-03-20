using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private SwordHitbox _swordHitbox;
    [SerializeField] private float _comboInputWindow = 0.8f; // Tiempo que acepta input

    private const int MaxCombo = 4;
    private int _comboCount = 0;
    private bool _isAttacking = false;
    private bool _inputReceived = false; // ¿pulsó en la ventana?
    private float _comboWindowTimer = 0f;
    private bool _windowOpen = false;

    private void Awake()
    {
        InputManager.Actions.Player.Attack.performed += OnAttackInput;
    }

    private void Update()
    {
        if (!_windowOpen) return;

        _comboWindowTimer += Time.deltaTime;

        // Si se acaba la ventana sin input, cerrarla
        if (_comboWindowTimer >= _comboInputWindow)
        {
            _windowOpen = false;
            _inputReceived = false;
        }
    }

    private void OnAttackInput(InputAction.CallbackContext ctx)
    {
        if (!_isAttacking)
        {
            // Primer ataque
            _isAttacking = true;
            _comboCount = 1;
            _inputReceived = false;
            NotifyAttack();
        }
        else if (_windowOpen && !_inputReceived && _comboCount < MaxCombo)
        {
            // Input dentro de la ventana → registrar para el siguiente ataque
            _inputReceived = true;
        }
    }

    // Llamado desde Animation Event al llegar al 60% de cada clip
    public void OpenComboWindow()
    {
        _windowOpen = true;
        _comboWindowTimer = 0f;
        _inputReceived = false;
    }

    // Llamado desde Animation Event al final de cada clip
    public void AttackFinished()
    {
        _swordHitbox.DisableHitbox();
        _windowOpen = false;

        if (_inputReceived && _comboCount < MaxCombo)
        {
            // Había input en la ventana → siguiente ataque
            _inputReceived = false;
            _comboCount++;
            NotifyAttack();
        }
        else
        {
            // No hubo input → volver a Idle
            ResetCombo();
        }
    }

    private void NotifyAttack()
    {
        Main.CustomEvents.OnAttackCombo?.Invoke(_comboCount);
        EnableHitbox();
    }

    private void ResetCombo()
    {
        _isAttacking = false;
        _inputReceived = false;
        _windowOpen = false;
        _comboCount = 0;
        _swordHitbox.DisableHitbox();
        Main.CustomEvents.OnComboReset?.Invoke();
    }

    // Animation Events en cada clip
    public void EnableHitbox() => _swordHitbox.EnableHitbox();

    private void OnDestroy()
    {
        InputManager.Actions.Player.Attack.performed -= OnAttackInput;
    }
}