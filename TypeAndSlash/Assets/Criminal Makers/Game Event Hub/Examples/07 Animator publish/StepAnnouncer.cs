using System.Collections;
using TMPro;
using UnityEngine;

namespace CriminalMakers.GameEventHub.Examples
{
    public class StepAnnouncer : MonoBehaviour
    {
        public TextMeshProUGUI countText;
        public CanvasGroup canvasGroup;


        private void OnEnable()
        {
            GameEventHub.Bind(this);
        }

        private void OnDisable()
        {
            GameEventHub.Unbind(this);
        }

        // Start is called before the first frame update
        void Start()
        {
            countText.text = "";
            StartCoroutine(FadeOutForever());
        }

        [OnGameEvent(SubscriberPriority.Essential)]
        public void OnPlayerStep(OnStep e)
        {
            // Ignore events from non-components
            if (e._emitter is not Component) return;

            var componentEmitter = (Component)e._emitter;

            // Ignore events from non-players
            if (!componentEmitter.gameObject.CompareTag("Player")) return;

            countText.text = $"Step {(e.isLeftFoot ? "left" : "right")} performed!";
            canvasGroup.alpha = 1;
        }

        IEnumerator FadeOutForever()
        {
            while (true)
            {
                canvasGroup.alpha -= 0.05f;
                if (canvasGroup.alpha <= 0)
                {
                    canvasGroup.alpha = 0;
                }

                yield return new WaitForSeconds(0.02f);
            }
        }
    }
}