using UnityEngine;

public class MeleeEnemy : EnemyBase
{
    [Header("Melee Config")]
    [SerializeField] private float _attackCooldown = 1.5f;

    private float _attackTimer = 0f;
    private bool _isAttacking = false;

    private bool _hasDealtDamage = false;


    protected override void HandleBehaviour()
    {
        if (IsPlayerInRange(_detectionRange))
        {
            if (IsPlayerInRange(_attackRange))
            {
                // Parar y atacar
                _agent.isStopped = true;
                _attackTimer += Time.deltaTime;

                if (_attackTimer >= _attackCooldown)
                {
                    _attackTimer = 0f;
                    StartAttack();
                }
            }
            else
            {
                // Perseguir
                _agent.isStopped = false;
                _agent.SetDestination(_player.position);
            }
        }
        else
        {
            // Fuera de rango — parar
            _agent.isStopped = true;
        }
    }

    public override void StartAttack()
    {
        _isAttacking = true;
        _hasDealtDamage = false; // reset al empezar cada ataque
        _animator?.SetTrigger("Attack");
    }

    public override void StopAttack()
    {
        _isAttacking = false;
        _hasDealtDamage = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && _isAttacking && !_hasDealtDamage)
        {
            _hasDealtDamage = true;
            DealDamage();
        }
    }

    // Animation Event al impactar
    public void DealDamage()
    {
        Debug.Log("MeleeEnemy: Intentando hacer daño al jugador...");
        if (IsPlayerInRange(_attackRange))
            Main.Player.TakeDamage(_attackDamage);
        Debug.Log($"Vida player: {Main.Player.Lives}");
    }

    // Animation Event al terminar el ataque
    public void AttackFinished()
    {
        StopAttack();
    }
}