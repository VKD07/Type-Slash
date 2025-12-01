using Code.Abstracts;
using CriminalMakers.GameEventHub;

namespace Code.GameEvents
{
    public class OnChosenSkillEvent : GameEvent
    {
        public SkillBase SkillBase;
        public int SlotIndex;

        public OnChosenSkillEvent(SkillBase skillBase, int slotIndex)
        {
            SkillBase = skillBase;
            SlotIndex = slotIndex;
        }
    }
}