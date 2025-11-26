using System;
using System.Collections;
using UnityEngine;

namespace CriminalMakers.GameEventHub.Examples
{
    [AddComponentMenu("")]
    public class OnKeyPressDynamicSubscriber : MonoBehaviour
    {
        public CanvasGroup canvasGroup;

        private void OnEnable()
        {
            StartCoroutine(ForeverFadeOut());
            StartCoroutine(ForeverSubAndUnsub());
        }
        
        IEnumerator ForeverFadeOut()
        {
            while (true)
            {
                canvasGroup.alpha -= Time.deltaTime;
                yield return null;
            }
        }

        IEnumerator ForeverSubAndUnsub()
        {
            while (true)
            {
                Action unsub = GameEventHub.Listen(this, (OnKeyPressed e) =>
                {
                    canvasGroup.alpha = 1;
                });
                
                yield return new WaitForSeconds(3);
                
                // Disconnect this object from the GameEventHub
                unsub();
                
                yield return new WaitForSeconds(3);
            }
        }
    }
}