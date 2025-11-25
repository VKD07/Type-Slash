using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace Code
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 10.0f;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private EnemySpawner _enemySpawner;
        
        [SerializeField] private TextMeshProUGUI _scoreText;

        private int _currentScore = 0;
        private Enemy _targetEnemy;
        private bool _hasTarget;

        private void Awake()
        {
            _rigidbody.bodyType = RigidbodyType2D.Dynamic;
            _rigidbody.gravityScale = 0;
            _rigidbody.linearVelocity = Vector2.zero;
        }

        private void Update()
        {
            KillEnemy();
            FollowTarget();
            _scoreText.text = _currentScore.ToString();
        }

        private void KillEnemy()
        {
            foreach (KeyControl key in Keyboard.current.allKeys)
            {
                if (key != null && key.wasPressedThisFrame)
                {
                    string keyName = key.displayName;
                    if (keyName.Length == 1)
                    {
                        char letter = char.ToLower(keyName[0]);
                        bool succesfullAttack = _enemySpawner.HandleTypedLetter(letter, this);
                        UpdateScore(succesfullAttack);
                    }
                }
            }
        }

        private void UpdateScore(bool succesfullAttack)
        {
            if (succesfullAttack)
            {
                _currentScore = _currentScore + 1;
            }
            else
            {
                _currentScore = 0;
            }
        }

        public void SetTargetEnemy(Enemy enemy)
        {
            if (enemy != null)
            {
                _targetEnemy = enemy;
                _hasTarget = true;
                _rigidbody.gravityScale = 0;
                _rigidbody.linearVelocity = Vector2.zero;
            }
        }

        private void FollowTarget()
        {
            if (!_hasTarget)
            {
                _rigidbody.gravityScale = 3;
                return;
            }

            if (_targetEnemy == null || !_targetEnemy.gameObject.activeInHierarchy)
            {
                _hasTarget = false;
                _rigidbody.gravityScale = 3;
                return;
            }

            Vector2 targetPos = _targetEnemy.transform.position;

            transform.position = Vector2.MoveTowards(
                transform.position,
                targetPos,
                _moveSpeed * Time.deltaTime
            );
        }
    }
}