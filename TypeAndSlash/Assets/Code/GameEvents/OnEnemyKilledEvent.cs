using CriminalMakers.GameEventHub;

namespace Code.GameEvents
{
    public class OnEnemyKilledEvent : GameEvent
    {
        public Enemy KilledEnemy;

        public OnEnemyKilledEvent(Enemy killedEnemy)
        {
            KilledEnemy = killedEnemy;
        }
    }
}