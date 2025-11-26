using System.Collections;
using CriminalMakers.GameEventHub.Utilities;
using UnityEngine;

namespace CriminalMakers.GameEventHub.Examples
{
    public class PublishContinuously : MonoBehaviour
    {
        public Vector2 minMaxRate = new Vector2(0.4f, 0.7f);

        [SubclassSelector, SerializeReference] public GameEvent gameEvent;

        public string channel;

        private void Start()
        {
            StartCoroutine(Publish());
        }

        private IEnumerator Publish()
        {
            while (true)
            {
                gameEvent.SetChannel(channel).Publish(this);
                float rate = Random.Range(minMaxRate.x, minMaxRate.y);
                yield return new WaitForSeconds(rate);
            }
        }
    }
}