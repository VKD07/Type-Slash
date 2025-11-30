using CriminalMakers.GameEventHub;

namespace Code.GameEvents
{
    public class OnKeyboardPressedEvent : GameEvent
    {
        public char KeyChar;
        public OnKeyboardPressedEvent(char letter)
        {
            KeyChar = letter;
        }
    }
}