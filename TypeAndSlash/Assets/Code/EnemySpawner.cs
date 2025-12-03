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
        [SerializeField] private Transform[] _spawnPoints;
        [SerializeField] private Vector2 _spawnRange;
        [SerializeField] private Enemy _enemyPrefab;
        [SerializeField] private Transform _spawnParent;
        [SerializeField] private Level[] _levels;

        private readonly List<Enemy> _activeEnemies = new List<Enemy>();
        private Words _currentWords;
        private Enemy _targetEnemy;
        private bool _canSpawn = true;
        private int _currentLevelIndex;
        private int _enemiesSpawnedThisLevel;
        private int _enemiesKilledThisLevel;

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
            if (_levels != null && _levels.Length > 0)
            {
                _currentWords = _levels[_currentLevelIndex].words;
            }

            while (true)
            {
                if (_canSpawn && CanSpawnEnemy())
                {
                    Vector2 range = GetCurrentSpawnRange();
                    yield return new WaitForSeconds(Random.Range(range.x, range.y));

                    Enemy enemy = Instantiate(
                        _enemyPrefab,
                        _spawnPoints[Random.Range(0, _spawnPoints.Length)].position,
                        Quaternion.identity,
                        _spawnParent
                    );

                    enemy.AssignAWord(_currentWords.GetRandomWord());
                    Register(enemy);
                    _enemiesSpawnedThisLevel++;
                }
                else
                {
                    yield return null;
                }
            }
        }

        private Vector2 GetCurrentSpawnRange()
        {
            if (_levels == null || _levels.Length == 0)
            {
                return _spawnRange;
            }

            if (_currentLevelIndex < 0 || _currentLevelIndex >= _levels.Length)
            {
                return _spawnRange;
            }

            return _levels[_currentLevelIndex].spawnTimeRange;
        }

        private bool CanSpawnEnemy()
        {
            if (_levels == null || _levels.Length == 0)
            {
                return true;
            }

            if (_currentLevelIndex < 0 || _currentLevelIndex >= _levels.Length)
            {
                return false;
            }

            Level level = _levels[_currentLevelIndex];
            return _enemiesSpawnedThisLevel < level.enemiesToSpawn;
        }

        private void Register(Enemy enemy)
        {
            _activeEnemies.Add(enemy);
        }

        public void PauseSpawning()
        {
            _canSpawn = false;
        }
        
        
        [OnGameEvent]
        public void ResumeSpawning(OnChosenSkillEvent e)
        {
            _canSpawn = true;
        }

        private void LevelUp()
        {
            _currentLevelIndex++;
            _enemiesSpawnedThisLevel = 0;
            _enemiesKilledThisLevel = 0;

            if (_currentLevelIndex < _levels.Length)
            {
                _currentWords = _levels[_currentLevelIndex].words;
            }
            else
            {
                _canSpawn = false;
            }
            
            PauseSpawning();
            new OnLeveUpEvent().Publish(this);
        }

        [OnGameEvent]
        private void OnEnemyWordFinished(OnEnemyKilledEvent e)
        {
            if (_targetEnemy == e.KilledEnemy)
            {
                _targetEnemy = null;
            }

            _activeEnemies.Remove(e.KilledEnemy);

            if (!string.IsNullOrWhiteSpace(e.KilledEnemy.AssignedWord))
            {
                _currentWords.AddWord(e.KilledEnemy.AssignedWord);
            }

            if (_levels != null && _levels.Length > 0 && _currentLevelIndex >= 0 && _currentLevelIndex < _levels.Length)
            {
                _enemiesKilledThisLevel++;

                Level level = _levels[_currentLevelIndex];

                if (_enemiesKilledThisLevel >= level.enemiesToSpawn)
                {
                    LevelUp();
                }
            }
        }

        [OnGameEvent]
        public void HandleTypedLetter(OnKeyboardPressedEvent pressed)
        {
            if (_targetEnemy != null && !_targetEnemy.gameObject.activeSelf)
            {
                _targetEnemy = null;
            }

            char key = char.ToLower(pressed.KeyChar);

            if (_targetEnemy != null)
            {
                string word = _targetEnemy.CurrentWord;
                if (!string.IsNullOrEmpty(word) && char.ToLower(word[0]) == key)
                {
                    new OnDamageEnemyByLetterEvent(_targetEnemy).Publish(this);
                    _targetEnemy.TakeDamageBasedOnLetter(pressed.KeyChar);
                }
                else
                {
                    new OnIncorrectLetterPressed().Publish(this);
                }

                return;
            }

            Enemy matchedEnemy = null;

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

                string w = e.CurrentWord;
                if (string.IsNullOrEmpty(w))
                {
                    continue;
                }

                if (char.ToLower(w[0]) == key)
                {
                    matchedEnemy = e;
                    break;
                }
            }

            if (matchedEnemy != null)
            {
                _targetEnemy = matchedEnemy;
                new OnDamageEnemyByLetterEvent(_targetEnemy).Publish(this);
                _targetEnemy.TakeDamageBasedOnLetter(pressed.KeyChar);
            }
            else
            {
                new OnIncorrectLetterPressed().Publish(this);
            }
        }

        [OnGameEvent]
        private void AssignRandomLettersToActiveEnemies(OnOneHitSuperSkillEvent e)
        {
            if (e.IsActive)
            {
                _canSpawn = false;

                for (int i = 0; i < e.AssignedWord.Length; i++)
                {
                    _currentWords.RemoveWordBasedoOnFirstLetter(e.AssignedWord[i]);
                }

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

    [System.Serializable]
    public class Level
    {
        public int enemiesToSpawn;
        public Vector2 spawnTimeRange;
        public Words words;
    }
}
