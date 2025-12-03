using Code.Interface;
using Code.UI;
using UnityEngine;

namespace Code.Abstracts
{
    public abstract class Damageable : MonoBehaviour, IDamageable
    {
        [SerializeField] protected float _health = 100f;
        [SerializeField] private HealthView _healthView;

        protected virtual void Awake()
        {
            _healthView.Setup(_health);
        }

        public virtual void TakeDamage(float damage)
        {
            if (_health <= 0)
            {
                Die();
                return;
            }
            _health -= damage;
            _healthView.UpdateValue(_health);
        }

        public void AddHealth(float val)
        {
            _health += val;
        }

        protected virtual void Die()
        {
        }
    }
}