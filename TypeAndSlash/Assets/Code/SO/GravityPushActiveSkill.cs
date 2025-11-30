using Code.Abstracts;
using UnityEngine;

namespace Code.SO
{
    [CreateAssetMenu(fileName = "GravityPushActiveSkill", menuName = "Skills/Active/GravityPushActiveSkill")]
    public class GravityPushActiveSkill : ActiveSkillBase
    {
        [SerializeField] private float _radius = 5f;
        [SerializeField] private float _forceStrength = 10f;
        [SerializeField] private LayerMask _enemyLayer;
        private Transform _playertranform;

        private Collider2D[] _detected;

        public override void OnAcquire()
        {
            _playertranform = _context.Player.transform;
        }

        protected override void OnActivate()
        {
            _detected = Physics2D.OverlapCircleAll(_playertranform.position, _radius, _enemyLayer);
            if (_detected.Length <= 0)
            {
                return;
            }

            foreach (Collider2D obj in _detected)
            {
                if (obj.TryGetComponent(out Enemy enemy))
                {
                    Vector2 dir = (enemy.transform.position - _playertranform.position).normalized;
                    enemy.KnockBack(dir, _forceStrength);
                }
            }
        }
    }
}