namespace CriminalMakers.GameEventHub.Examples
{
    public class OnCounter: GameEvent
    {
        public int counter;

        public OnCounter()
        {
        }

        public OnCounter(int counter)
        {
            this.counter = counter;
        }
    }
}