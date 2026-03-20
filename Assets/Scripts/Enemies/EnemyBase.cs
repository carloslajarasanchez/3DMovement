using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBase : MonoBehaviour, IEnemy
{
    [Header("Stats")]
    [SerializeField] protected float _maxHealth = 100f;
    [SerializeField] protected float _attackDamage = 10f;
    [SerializeField] protected float _attackRange = 2f;
    [SerializeField] protected float _detectionRange = 10f;

    protected float _currentHealth;
    protected NavMeshAgent _agent;
    protected Transform _player;
    protected Animator _animator;
    protected bool _isDead = false;

    protected virtual void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _currentHealth = _maxHealth;
    }

    protected virtual void Start()
    {
        // Buscar al jugador por tag
        _player = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        if (_isDead) return;
        HandleBehaviour();
        UpdateAnimator();
    }

    private void UpdateAnimator()
    {
        // Velocidad normalizada del NavMeshAgent entre 0 y 1
        float velocity = _agent.velocity.magnitude / _agent.speed;
        _animator?.SetFloat("Velocity", velocity);
    }

    // Cada enemigo implementa su propio comportamiento
    protected abstract void HandleBehaviour();

    public virtual void TakeDamage(float damage)
    {
        if (_isDead) return;
        _currentHealth -= damage;

        _animator?.SetTrigger("Hit");
        Debug.Log($"{gameObject.name} took {damage} damage. Current health: {_currentHealth}");

        if (_currentHealth <= 0)
            Die();
    }

    public virtual void Die()
    {
        _isDead = true;
        _agent.isStopped = true;
        _animator?.SetTrigger("Die");
        Main.CustomEvents.OnEnemyDied?.Invoke(gameObject);
    }

    public abstract void StartAttack();
    public abstract void StopAttack();

    protected bool IsPlayerInRange(float range)
    {
        return Vector3.Distance(transform.position, _player.position) <= range;
    }

    public virtual void DieAnimationFinished()
    {
        Destroy(gameObject, .05f);
    }
}