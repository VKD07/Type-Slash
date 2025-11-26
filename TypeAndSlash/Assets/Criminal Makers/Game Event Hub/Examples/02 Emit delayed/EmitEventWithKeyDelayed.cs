using UnityEngine;

namespace CriminalMakers.GameEventHub.Examples
{
    [AddComponentMenu("")]
    public class EmitEventWithKeyDelayed : MonoBehaviour
    {
        public float secondsToWait = 0.5f;
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                new OnKeyPressed().PublishDelayed(this, secondsToWait);
            }
        }
    }
}