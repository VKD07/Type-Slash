using System.Collections;
using UnityEngine;

namespace CriminalMakers.GameEventHub.Examples
{
    [AddComponentMenu("")]
    public class OnKeyPressSubscriber : MonoBehaviour
    {
        public CanvasGroup canvasGroup;

        private void OnEnable()
        {
            // Connect this object to the GameEventHub
            GameEventHub.Bind(this);
            StartCoroutine(ForeverFadeOut());

        }

        private void OnDisable()
        {
            // Disconnect this object from the GameEventHub
            GameEventHub.Unbind(this);
        }

        [OnGameEvent(SubscriberPriority.Medium)]
        private void OnKeyPressed(OnKeyPressed e)
        {
            canvasGroup.alpha = 1;
        }

        IEnumerator ForeverFadeOut()
        {
            while (true)
            {
                canvasGroup.alpha -= Time.deltaTime;
                yield return null;
            }
        }
    }
}