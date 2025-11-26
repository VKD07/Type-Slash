using CriminalMakers.GameEventHub.Examples;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CriminalMakers.GameEventHub.Demo
{
    [AddComponentMenu("")]
    public class Passenger : MonoBehaviour
    {
        public TextMeshProUGUI actionBubbleText;

        private void OnEnable()
        {
            // Add your binding code here
        }

        private void OnDisable()
        {
            // Add your unbinding code here
        }

        void Start()
        {
            int random = Random.Range(0, 3);
            actionBubbleText.color = Color.black;

            switch (random)
            {
                case 0:
                {
                    actionBubbleText.text = "Checking phone";
                    break;
                }
                case 1:
                {
                    actionBubbleText.text = "Talking to someone";
                    break;
                }
                case 2:
                {
                    actionBubbleText.text = "Whistling";
                    break;
                }
            }
        }

        // Add your event handler here
    }
}