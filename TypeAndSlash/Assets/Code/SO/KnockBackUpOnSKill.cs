using Code.Abstracts;
using Code.Interface;
using UnityEngine;

namespace Code.SO
{
    [CreateAssetMenu(fileName = "KnockbackOnLetter", menuName = "Skills/Passive/Knockback On Letter")]
    public class KnockBackUpOnSKill : PassiveSkillBase, IActivateOnEnemyDamaged
    {
        [SerializeField] private float knockbackForce = 1f;
        
        public void OnEnemyDamaged(Enemy enemy)
        {
            enemy.Move(Vector3.up, knockbackForce);
        }
    }
}