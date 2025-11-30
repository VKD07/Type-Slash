namespace Code.Abstracts
{
    public abstract class SuperSkillBase : SkillBase
    {
        private bool _isActive;

        public override void Activate()
        {
            _isActive = true;
            OnSuperStart();
        }

        public override void Deactivate()
        {
            _isActive = false;
            OnSuperEnd();
        }

        public override void Tick(float deltaTime, float duration)
        {
            if (!_isActive) return;
            if (duration <= 0f)
            {
                Deactivate();
                return;
            }

            OnSuperTick(deltaTime, duration);
        }

        protected abstract void OnSuperStart();
        protected abstract void OnSuperTick(float deltaTime, float duration);
        protected abstract void OnSuperEnd();
    }
}