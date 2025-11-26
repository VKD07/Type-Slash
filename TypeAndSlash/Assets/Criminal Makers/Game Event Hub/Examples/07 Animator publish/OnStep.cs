using UnityEngine;

namespace CriminalMakers.GameEventHub.Examples
{
    public class OnStep : GameEvent
    {
        public Animator animator;
        public bool isLeftFoot;

        public OnStep()
        {
        }

        public OnStep(bool isLeftFoot)
        {
            this.isLeftFoot = isLeftFoot;
        }
    }
}