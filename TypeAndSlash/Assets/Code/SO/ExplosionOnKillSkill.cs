using Code.Abstracts;
using Code.Interface;
using UnityEngine;

namespace Code.SO
{
    [CreateAssetMenu(fileName = "ExplosionOnKill", menuName = "Skills/Passive/Explosion On Kill")]
    public class ExplosionOnKillSkill : PassiveSkillBase, IActivateOnEnemyKilled
    {
        [SerializeField] private float _explosionRadius = 3f;
        [SerializeField] private int _explosionDamage = 4;
        [SerializeField] private float _chanceOfExplosion = .5f;

        public void OnEnemyKilled(Enemy enemy)
        {
            if (Random.value > _chanceOfExplosion)
            {
                return;
            }
            
            Collider2D[] hits = Physics2D.OverlapCircleAll(
                enemy.transform.position,
                _explosionRadius
            );

            foreach (Collider2D hit in hits)
            {
                if (hit != null &&
                    hit.TryGetComponent(out Enemy e) &&
                    e != enemy)
                {
                    e.TakeDamage(_explosionDamage);
                }
            }
        }

    }
}