using Code.GameEvents;
using UnityEngine;
namespace Code
{
    public class EnemyKiller : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
         

        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.GetComponent<Player>() != null)
            {
               // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            }
            
            if (other.gameObject.GetComponent<Enemy>() != null)
            {
                new OnEnemyKilledEvent(other.gameObject.GetComponent<Enemy>()).Publish(this);
                other.gameObject.SetActive(false);
            }
        }
    }
}