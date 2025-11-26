using UnityEngine;

namespace CriminalMakers.GameEventHub.Examples
{
    [AddComponentMenu("")]
    public class BusHonkPlayer : MonoBehaviour
    {
        public AudioSource audioSource;

        private void OnEnable()
        {
            GameEventHub.Bind(this);
        }

        private void OnDisable()
        {
            GameEventHub.Unbind(this);
        }

        // We are using essential priority, so it doesn't get affected by filters
        [OnGameEvent(SubscriberPriority.Essential)]
        private void PlayBusHonk(OnBusHonk e)
        {
            audioSource.Play();
        }
    }
}