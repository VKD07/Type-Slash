using UnityEngine;

namespace CriminalMakers.GameEventHub.Examples
{
    [AddComponentMenu("")]
    public class Bus2 : MonoBehaviour
    {
        public float speed = 5f;
        public Collider2D zoneToEmitEvent2D;

        private void Update()
        {
            if (Input.GetKey(KeyCode.A))
            {
                transform.position += Vector3.left * (speed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.D))
            {
                transform.position += Vector3.right * (speed * Time.deltaTime);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                // Honk the horn so the passengers get up in the bus
                new OnBusHonk()
                    .WithFilter(new InsideCollider2D(zoneToEmitEvent2D))
                    .Publish(this);
            }
        }
    }
}