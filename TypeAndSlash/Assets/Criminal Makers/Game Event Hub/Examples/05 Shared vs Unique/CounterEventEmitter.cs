using UnityEngine;

namespace CriminalMakers.GameEventHub.Examples
{
    [AddComponentMenu("")]
    public class CounterEventEmitter: MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                new OnCounter()
                    .Shared()
                    .Publish(this);
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                new OnCounter()
                    .Publish(this);
            }
        }
    }
}