using Code.Abstracts;
using UnityEngine;

namespace Code.SO
{
    [CreateAssetMenu (fileName = "TrailFrostPassive", menuName = "Skills/Passive/TrailFrost")]
    public class TrailFrost : PassiveSkillBase
    {
        [SerializeField] private float _speedReduction = 5.0f;
        private Transform _playerTransform;
        public override void OnAcquire()
        {
            _playerTransform = _context.Player.transform;
        }

        public override void Tick(float deltaTime)
        {
            Collider2D [] detected = Physics2D.OverlapCircleAll (_playerTransform.position, 0.2f);

            foreach (Collider2D obj in detected)
            {
                if (obj.TryGetComponent(out Enemy enemy))
                {
                    enemy.ReduceMoveSpeed(_speedReduction);
                }
            }
        }
    }
}