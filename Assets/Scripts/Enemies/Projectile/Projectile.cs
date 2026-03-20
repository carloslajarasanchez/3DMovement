using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _lifetime = 5f;

    private Transform _target;
    private float _damage;

    public void Init(Transform target, float damage)
    {
        _target = target;
        _damage = damage;
        Destroy(gameObject, _lifetime);
    }

    private void Update()
    {
        if (_target == null)
        {
            Destroy(gameObject);
            return;
        }

        // Moverse hacia el jugador
        Vector3 direction = (_target.position - transform.position).normalized;
        transform.position += direction * _speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Main.Player.TakeDamage(_damage);
            Destroy(gameObject);
        }
    }
}