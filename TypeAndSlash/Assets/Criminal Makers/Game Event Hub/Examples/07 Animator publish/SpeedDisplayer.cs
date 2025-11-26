using TMPro;
using UnityEngine;

namespace CriminalMakers.GameEventHub.Examples
{
    public class SpeedDisplayer : MonoBehaviour
    {
        public TextMeshProUGUI speedText;

        private void OnEnable()
        {
            speedText.text = $"Speed parameter: 0";
            GameEventHub.Bind(this);
        }

        private void OnDisable()
        {
            GameEventHub.Unbind(this);
        }

        [OnGameEvent(SubscriberPriority.Essential)]
        public void UpdateSpeedMessage(OnStep e)
        {
            // Ignore events from non-components
            if (e._emitter is not Component) return;

            var componentEmitter = (Component)e._emitter;

            // Ignore events from non-players
            if (!componentEmitter.gameObject.CompareTag("Player")) return;

            var speedValue = e.animator.GetFloat("speed");
            speedText.text = $"Speed parameter: {speedValue}";
        }
    }
}