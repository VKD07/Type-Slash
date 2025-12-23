using Code.GameEvents;
using CriminalMakers.GameEventHub;
using Fusion;
using TMPro;
using UnityEngine;

namespace Code.UI
{
    public class WinnerView : NetworkBehaviour
    {
        [SerializeField] private TextMeshProUGUI _winnerText;

        [Networked, OnChangedRender(nameof(SetWinnerText))]
        public NetworkString<_32> Winner { get; set; }

        private void Awake()
        {
            _winnerText.gameObject.SetActive(false);
            GameEventHub.Bind(this);
        }

        private void OnDestroy()
        {
            GameEventHub.Unbind(this);
        }

        private void SetWinnerText()
        {
            _winnerText.text = Winner + " Wins!";
            _winnerText.gameObject.SetActive(true);
        }

        [OnGameEvent]
        private void OnWinnerDeclaredEvent(OnWinnerDeclared onWinnerDeclared)
        {
            if (Object.HasStateAuthority)
            {
                Winner = onWinnerDeclared.PlayerName;
            }
        }
    }
}