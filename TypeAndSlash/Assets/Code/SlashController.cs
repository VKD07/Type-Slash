using Code.GameEvents;
using CriminalMakers.GameEventHub;
using UnityEngine;

namespace Code
{
    public class SlashController : MonoBehaviour
    {
        [SerializeField] private ParticleSystem[] _particles;

        private void Awake()
        {
            GameEventHub.Bind(this);
        }

        private void OnDestroy()
        {
            GameEventHub.Unbind(this);
        }

        [OnGameEvent]
        private void OnDamageEnemy(OnDamageEnemyByLetterEvent e)
        {
            SetTransform(e.Enemy.transform.position);
        }

        public void SetTransform(Vector2 position)
        {
            transform.position = position;
            _particles[Random.Range(0, _particles.Length)].Play();
        }

        public void PlayAllParticles()
        {
            foreach (ParticleSystem particle in _particles)
            {
                particle.Play();
            }
        }
    }
}