using CriminalMakers.GameEventHub;

namespace Code.GameEvents
{
    public class OnOneHitSuperSkillEvent : GameEvent
    {
        public bool IsActive;
        public string AssignedWord;

        public OnOneHitSuperSkillEvent(bool isActive, string assignedWord)
        {
            IsActive = isActive;
            AssignedWord = assignedWord;
        }
    }
}