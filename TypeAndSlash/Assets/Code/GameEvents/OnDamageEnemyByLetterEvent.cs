using CriminalMakers.GameEventHub;

namespace Code.GameEvents
{
    public class OnDamageEnemyByLetterEvent : GameEvent
    {
        public Enemy Enemy;
        public OnDamageEnemyByLetterEvent(Enemy enemy = null)
        {
            Enemy = enemy;
        }
    }
}