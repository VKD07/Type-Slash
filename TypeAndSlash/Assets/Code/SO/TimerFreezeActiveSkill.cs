using UnityEngine;
using Code.Abstracts;
using Code.GameEvents;

namespace Code.SO
{
    [CreateAssetMenu (fileName = "TimerFreezeActiveSkill", menuName = "Skills/Active/TimerFreezeActiveSkill")]
    public class TimerFreezeActiveSkill : ActiveSkillBase
    {
        [SerializeField] private float freezeDuration = 5f;

        private float _freezeTimer = 0f;
        private bool _isActive = false;

        protected override void OnActivate()
        {
            _isActive = true;
            _freezeTimer = freezeDuration;
            FreezeAllEnemies();
        }

        public override void Tick(float deltaTime)
        {
            base.Tick(deltaTime);

            if (_isActive)
            {
                _freezeTimer -= deltaTime;
                if (_freezeTimer <= 0f)
                {
                    _isActive = false;
                    UnfreezeAllEnemies();
                }
            }
        }

        private void FreezeAllEnemies()
        {
            new OnEnemyMoveSpeedModified(1f).Publish(this);
        }

        private void UnfreezeAllEnemies()
        {
            new OnEnemyMoveSpeedModified(true).Publish(this);
        }
    }
}