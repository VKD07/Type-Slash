using UnityEngine;

namespace CriminalMakers.GameEventHub.Examples
{
    [AddComponentMenu("")]
    public class EventEmitOnKey : MonoBehaviour
    {
        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                new OnKeyPressed().Publish(this);
            }
        }
    }
}