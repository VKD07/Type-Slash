using Code.Interface;
using UnityEngine;

namespace Code.Abstracts
{
    public abstract class SkillBase : ScriptableObject, ISkill
    {
        [SerializeField] private Sprite _sprite;
        [SerializeField] protected string _skillName;
        [SerializeField] protected string _description;

        protected SkillContext _context;

        public virtual void Initialize(SkillContext ctx)
        {
            _context = ctx;
        }

        public virtual void OnAcquire() { }
        public virtual void OnRemove() { }
        public virtual void Tick(float deltaTime) { }
        public virtual void Tick(float deltaTime, float duration) { }

        public virtual void Activate() { }
        public virtual void Deactivate() { }
    }
}