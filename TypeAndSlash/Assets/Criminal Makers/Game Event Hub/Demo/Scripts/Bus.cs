using CriminalMakers.GameEventHub.Examples;
using UnityEngine;

namespace CriminalMakers.GameEventHub.Demo
{
    [AddComponentMenu("")]
    public class Bus : MonoBehaviour
    {
        public float speed = 5f;

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
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("bus-stop"))
            {
                // Add your event here when bus arrives at the stop
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("bus-stop"))
            {
                // When bus leaves the stop, add your event here
            }
        }
    }
}