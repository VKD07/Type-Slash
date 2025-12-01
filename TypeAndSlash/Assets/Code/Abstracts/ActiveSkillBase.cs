using System;
using UnityEngine;

namespace Code.Abstracts
{
    public abstract class ActiveSkillBase : SkillBase
    {
        [SerializeField] protected float cooldown = 5f;

        protected float cooldownTimer = 0f;
        
        [NonSerialized] public float GetNormalizedCooldownTimer;

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