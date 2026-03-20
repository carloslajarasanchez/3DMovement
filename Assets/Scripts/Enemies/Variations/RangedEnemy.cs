using UnityEngine;

public class RangedEnemy : EnemyBase
{
    [Header("Ranged Config")]
    [SerializeField] private float _preferredDistance = 8f;
    [SerializeField] private float _attackCooldown = 2f;
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private Transform _firePoint;

    private float _attackTimer = 0f;

    protected override void HandleBehaviour()
    {
        if (!IsPlayerInRange(_detectionRange)) return;

        float distanceToPlayer = Vector3.Distance(transform.position, _player.position);

        // Mirar siempre al jugador
        Vector3 direction = (_player.position - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

        if (distanceToPlayer < _preferredDistance)
        {
            // Demasiado cerca — retroceder
            _agent.isStopped = false;
            _agent.SetDestination(transform.position - direction * 2f);
            _animator?.SetBool("IsWalking", true);
        }
        else
        {
            // Distancia correcta — disparar
            _agent.isStopped = true;
            _animator?.SetBool("IsWalking", false);
            _attackTimer += Time.deltaTime;

            if (_attackTimer >= _attackCooldown)
            {
                _attackTimer = 0f;
                StartAttack();
            }
        }
    }

    public override void StartAttack()
    {
        _animator?.SetTrigger("Attack");
        SpawnProjectile();
    }

    public override void StopAttack() { }

    private void SpawnProjectile()
    {
        if (_projectilePrefab == null || _firePoint == null) return;

        GameObject projectile = Instantiate(_projectilePrefab, _firePoint.position, _firePoint.rotation);
        if (projectile.TryGetComponent<Projectile>(out var proj))
            proj.Init(_player, _attackDamage);
    }
}