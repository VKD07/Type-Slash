using Code.Interface;
using UnityEngine;

namespace Code.Abstracts
{
    public abstract class SkillBase : ScriptableObject, ISkill
    {
        public Sprite Sprite;
        public string SkillName;
        public string Description;

        protected SkillContext _context;

        public virtual void Initialize(SkillContext ctx)
        {
            _context = ctx;
        }

        public virtual void OnAcquire()
        {
        }

        public virtual void OnRemove()
        {
        }

        public virtual void Tick(float deltaTime)
        {
        }

        public virtual void Tick(float deltaTime, float duration)
        {
        }

        public virtual void Activate()
        {
        }

        public virtual void Deactivate()
        {
        }
    }
}