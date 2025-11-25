using UnityEngine;

namespace Code
{
    public class SlashController : MonoBehaviour
    {
        [SerializeField] private ParticleSystem[] _particles;

        public void SetTransform(Vector2 position)
        {
            transform.position = position;
            _particles[Random.Range(0, _particles.Length)].Play();
        }
    }
}