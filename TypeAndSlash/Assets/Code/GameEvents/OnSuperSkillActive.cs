using CriminalMakers.GameEventHub;

namespace Code.GameEvents
{
    public class OnSuperSkillActive : GameEvent
    {
        public bool IsActive;

        public OnSuperSkillActive(bool isActive)
        {
            IsActive = isActive;
        }
    }
}