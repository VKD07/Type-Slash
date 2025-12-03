using Code.Abstracts;
using Code.GameEvents;
using Code.Interface;
using UnityEngine;
namespace Code
{
    public class Base : Damageable
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out Enemy enemy))
            {
                new OnEnemyKilledEvent(other.gameObject.GetComponent<Enemy>()).Publish(this);
                if (enemy is IDamageDealer damageDealer)
                {
                    TakeDamage(damageDealer.Damage);
                }
                enemy.gameObject.SetActive(false);
            }
        }
    }
}