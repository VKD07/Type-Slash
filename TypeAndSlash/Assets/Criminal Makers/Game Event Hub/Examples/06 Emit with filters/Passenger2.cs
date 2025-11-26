using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CriminalMakers.GameEventHub.Examples
{
    [AddComponentMenu("")]
    public class Passenger2: MonoBehaviour
    {
        public TextMeshProUGUI actionBubbleText;

        private void OnEnable()
        {
            GameEventHub.Bind(this);
        }

        private void OnDisable()
        {
            GameEventHub.Unbind(this);
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
        
        [OnGameEvent]
        private void OnBusHonk(OnBusHonk e)
        {
            actionBubbleText.color = Color.red;
            actionBubbleText.text = "Boarding the bus";
            
            Destroy(gameObject, 3f);
        }
    }
}