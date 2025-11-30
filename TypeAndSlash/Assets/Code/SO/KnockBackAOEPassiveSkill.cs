using Code.Abstracts;
using Code.Interface;
using UnityEngine;

namespace Code.SO
{
    [CreateAssetMenu (menuName = "Skills/Passive/KnockBackAOE", fileName = "KnockBackAOEPassiveSkill")]
    public class KnockBackAOEPassiveSkill : PassiveSkillBase, IActivateOnEnemyDamaged
    {
        [SerializeField] private float _radius = 2.0f;
        [SerializeField] private float _strength = 1.0f;
        [SerializeField] private float _chances = 0.3f;

        public void OnEnemyDamaged(Enemy enemy)
        {
            if (Random.value > _chances)
            {
                return;
            }
            
            Collider2D[] hits = Physics2D.OverlapCircleAll(
                enemy.transform.position,
                _radius
            );

            foreach (Collider2D hit in hits)
            {
                if (hit != null &&
                    hit.TryGetComponent(out Enemy e) &&
                    e != enemy)
                {
                    e.KnockBack(Vector3.up, _strength);
                }
            }
        }
    }
}