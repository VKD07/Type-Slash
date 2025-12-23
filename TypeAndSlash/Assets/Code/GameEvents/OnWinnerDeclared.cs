using CriminalMakers.GameEventHub;

namespace Code.GameEvents
{
    public class OnWinnerDeclared : GameEvent
    {
        public string PlayerName;

        public OnWinnerDeclared(string playerName)
        {
            PlayerName = playerName;
        }
    }
}