using TMPro;
using UnityEngine;

namespace CriminalMakers.GameEventHub.Examples
{
    public class StepCounter : MonoBehaviour
    {
        public TextMeshProUGUI countText;

        private int stepCount = 0;

        private void OnEnable()
        {
            GameEventHub.Bind(this);
        }

        private void OnDisable()
        {
            GameEventHub.Unbind(this);
        }

        private void Start()
        {
            countText.text = "Step count: 0";
        }

        [OnGameEvent(SubscriberPriority.Essential)]
        private void OnPlayerStep(OnStep e)
        {
            // Ignore events from non-components
            if (e._emitter is not Component) return;

            var componentEmitter = (Component)e._emitter;

            // Ignore events from non-players
            if (!componentEmitter.gameObject.CompareTag("Player")) return;

            stepCount++;
            countText.text = $"Step count: {stepCount}";
        }
    }
}