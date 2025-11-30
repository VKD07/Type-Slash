using CriminalMakers.GameEventHub;

namespace Code.GameEvents
{
    public class OnEnemyMoveSpeedModified : GameEvent
    {
        public float NewMoveSpeed;
        public bool Reset = false;

        public OnEnemyMoveSpeedModified(float newMoveSpeed)
        {
            NewMoveSpeed = newMoveSpeed;
        }

        public OnEnemyMoveSpeedModified(bool reset)
        {
            Reset = reset;
        }
    }
}