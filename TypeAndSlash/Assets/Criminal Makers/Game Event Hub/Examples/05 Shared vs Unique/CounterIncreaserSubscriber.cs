using UnityEngine;

namespace CriminalMakers.GameEventHub.Examples
{
    [AddComponentMenu("")]
    public class CounterIncreaserSubscriber : MonoBehaviour
    {
        private void OnEnable()
        {
            GameEventHub.Bind(this);
        }

        private void OnDisable()
        {
            GameEventHub.Unbind(this);
        }

        [OnGameEvent]
        private void IncreaseCounter(OnCounter e)
        {
            e.counter++;
            Debug.Log("Counter increased to " + e.counter);
        }
    }
}