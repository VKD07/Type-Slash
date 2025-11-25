using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private Words _words;
        [SerializeField] private Transform[] _spawnPoints;
        [SerializeField] private Vector2 _spawnRange;
        [SerializeField] private Enemy _enemyPrefab;
        [SerializeField] private Transform _spawnParent;
        [SerializeField] private float _upSpeed = 0.5f;
        [SerializeField] private float _knockBackForce = 0.2f;

        private Player _player;
        private readonly List<Enemy> _activeEnemies = new List<Enemy>();
        private Enemy _lockedEnemy;

        private IEnumerator Start()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(_spawnRange.x, _spawnRange.y));

                Enemy enemy = Instantiate(
                    _enemyPrefab,
                    _spawnPoints[Random.Range(0, _spawnPoints.Length)].position,
                    Quaternion.identity,
                    _spawnParent
                );

                
                enemy.AssignAWord(_words.GetRandomWord());
                Register(enemy);
            }
        }

        private void Register(Enemy enemy)
        {
            _activeEnemies.Add(enemy);
            enemy.OnDeath += e => _activeEnemies.Remove(e);
            enemy.OnWordFinished += OnEnemyWordFinished;
        }

        private void OnEnemyWordFinished(Enemy enemy)
        {
            if (_lockedEnemy == enemy)
            {
                _words.AddWord(_lockedEnemy.CurrentWord);
                _lockedEnemy = null;
            }
        }

        private bool IsVisible(Enemy enemy)
        {
            Vector3 vp = Camera.main.WorldToViewportPoint(enemy.transform.position);
            return vp.x > 0 && vp.x < 1 && vp.y > 0 && vp.y < 1 && vp.z > 0;
        }

        public bool HandleTypedLetter(char letter, Player player)
        {
            if (_player == null)
            {
                _player = player;
            }
            
            if (_lockedEnemy != null && !_lockedEnemy.gameObject.activeSelf)
            {
                _lockedEnemy = null;
            }

            if (_lockedEnemy == null)
            {
                foreach (Enemy e in _activeEnemies)
                {
                    if (e == null)
                    {
                        continue;
                    }

                    if (!e.gameObject.activeInHierarchy)
                    {
                        continue;
                    }

                    if (!IsVisible(e))
                    {
                        continue;
                    }

                    string w = e.CurrentWord;
                    if (w.Length > 0 && char.ToLower(w[0]) == letter)
                    {
                        _lockedEnemy = e;
                        break;
                    }
                }
            }

            if (_lockedEnemy != null && !string.IsNullOrEmpty(_lockedEnemy.CurrentWord) &&
                char.ToLower(_lockedEnemy.CurrentWord[0]) == letter)
            {
                _lockedEnemy.KnockBackUp(_knockBackForce);
                _lockedEnemy.Damage(letter);
                _player.SetTargetEnemy(_lockedEnemy);
                return true;
            }

            return false;
        }
    }
}