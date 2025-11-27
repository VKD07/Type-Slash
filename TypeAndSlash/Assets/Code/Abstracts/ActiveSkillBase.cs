using UnityEngine;

namespace Code.Abstracts
{
    public abstract class ActiveSkillBase : SkillBase
    {
        [SerializeField] protected float cooldown = 5f;
        protected float cooldownTimer = 0f;

        public override void Tick(float deltaTime)
        {
            if (cooldownTimer > 0f)
            {
                cooldownTimer -= deltaTime;
            }
        }

        public override void Activate()
        {
            if (cooldownTimer > 0f)
            {
                return;
            }

            OnActivate();
            cooldownTimer = cooldown;
        }

        protected abstract void OnActivate();
    }
}