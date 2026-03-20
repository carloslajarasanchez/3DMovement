using System.Collections.Generic;
using UnityEngine;

public class SwordHitbox : MonoBehaviour
{
    private bool _isActive = false;
    private float _damage = 10f;
    // Guardar enemigos ya golpeados en este ataque
    private readonly HashSet<IEnemy> _hitEnemies = new HashSet<IEnemy>();

    public void EnableHitbox()
    {
        _isActive = true;
        _hitEnemies.Clear(); // limpiar al empezar cada ataque
    }

    public void DisableHitbox()
    {
        _isActive = false;
        _hitEnemies.Clear();
    }

    public void SetDamage(float damage) => _damage = damage;

    private void OnTriggerEnter(Collider other)
    {
        if (!_isActive) return;

        // Buscar IEnemy en el objeto o en su padre
        IEnemy enemy = other.GetComponent<IEnemy>()
                     ?? other.GetComponentInParent<IEnemy>();

        if (enemy == null) return;

        // Si ya golpeamos a este enemigo en este ataque, ignorar
        if (_hitEnemies.Contains(enemy)) return;

        _hitEnemies.Add(enemy);
        enemy.TakeDamage(_damage);
    }

    private void UpdateDamage()// Para cuando se suba de nivel o se cambie el arma, actualizar el daño del hitbox
    {
        _damage = Main.Player.Damage;
    }
}