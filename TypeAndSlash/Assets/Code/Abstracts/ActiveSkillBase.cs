using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.Abstracts
{
    public abstract class ActiveSkillBase : SkillBase
    {
        [SerializeField] private Key key;
        [SerializeField] protected float cooldown = 5f;

        protected float cooldownTimer = 0f;
        
        public Key ActivationKey => key;

        public float GetNormalizedCooldownTimer;

        public override void Tick(float deltaTime)
        {
            if (cooldownTimer > 0f)
            {
                cooldownTimer -= deltaTime;
                GetNormalizedCooldownTimer = cooldownTimer/ cooldown;
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