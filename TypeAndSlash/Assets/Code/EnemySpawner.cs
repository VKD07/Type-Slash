using System.Collections;
using System.Collections.Generic;
using Code.GameEvents;
using CriminalMakers.GameEventHub;
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

        private readonly List<Enemy> _activeEnemies = new List<Enemy>();
        private Enemy _targetEnemy;
        private bool _canSpawn = true;

        private void Awake()
        {
            GameEventHub.Bind(this);
        }

        private void OnDestroy()
        {
            GameEventHub.Unbind(this);
        }

        private IEnumerator Start()
        {
            while (true)
            {
                if (_canSpawn)
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
                else
                {
                    yield return null;
                }
            }
        }

        private void Register(Enemy enemy)
        {
            _activeEnemies.Add(enemy);
        }

        public void PauseSpawning()
        {
            _canSpawn = false;
        }

        public void ResumeSpawning()
        {
            _canSpawn = true;
        }

        [OnGameEvent]
        private void OnEnemyWordFinished(OnEnemyKilledEvent e)
        {
            if (_targetEnemy == e.KilledEnemy)
            {
                _activeEnemies.Remove(e.KilledEnemy);

                if (!string.IsNullOrWhiteSpace(_targetEnemy.AssignedWord))
                    _words.AddWord(_targetEnemy.AssignedWord);

                _targetEnemy = null;
            }
        }

        [OnGameEvent]
        public void HandleTypedLetter(OnKeyboardPressedEvent pressed)
        {
            if (_targetEnemy != null && !_targetEnemy.gameObject.activeSelf)
                _targetEnemy = null;

            if (_targetEnemy == null)
            {
                foreach (Enemy e in _activeEnemies)
                {
                    if (e == null)
                        continue;

                    if (!e.gameObject.activeInHierarchy)
                        continue;

                    string w = e.CurrentWord;
                    if (w.Length > 0 && char.ToLower(w[0]) == pressed.KeyChar)
                    {
                        _targetEnemy = e;
                        break;
                    }
                }
            }

            if (_targetEnemy != null &&
                !string.IsNullOrEmpty(_targetEnemy.CurrentWord) &&
                char.ToLower(_targetEnemy.CurrentWord[0]) == pressed.KeyChar)
            {
                new OnDamageEnemyByLetterEvent(_targetEnemy).Publish(this);
                _targetEnemy.TakeDamageBasedOnLetter(pressed.KeyChar);
            }
        }

        [OnGameEvent]
        private void AssignRandomLettersToActiveEnemies(OnOneHitSuperSkillEvent e)
        {
            if (e.IsActive)
            {
                _canSpawn = false;
                for (int i = 0; i < _activeEnemies.Count; i++)
                {
                    _activeEnemies[i].AssignNewWord(e.AssignedWord[i].ToString());
                }

                return;
            }
            
            for (int i = 0; i < _activeEnemies.Count; i++)
            {
                _activeEnemies[i].AssignNewWord(_activeEnemies[i].CurrentWord);
            }
            _canSpawn = true;
        }
    }
}