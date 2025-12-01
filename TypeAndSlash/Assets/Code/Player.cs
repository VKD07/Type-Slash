using Code.GameEvents;
using CriminalMakers.GameEventHub;
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
        [SerializeField] private SlashController _slashController;

        private Transform _target;
        private Vector2 _targetPosition;
        private bool _hasTarget;

        private void Awake()
        {
            _rigidbody.bodyType = RigidbodyType2D.Dynamic;
            _rigidbody.gravityScale = 0;
            _rigidbody.linearVelocity = Vector2.zero;
            GameEventHub.Bind(this);
        }

        private void OnDestroy()
        {
            GameEventHub.Unbind(this);
        }

        private void Update()
        {
            PressKeyboard();
            FollowTarget();
        }

        private void PressKeyboard()
        {
            foreach (KeyControl key in Keyboard.current.allKeys)
            {
                if (key != null && key.wasPressedThisFrame)
                {
                    string keyName = key.displayName;
                    if (keyName.Length == 1)
                    {
                        char letter = char.ToLower(keyName[0]);
                        if(char.IsDigit(letter))
                        {
                            continue;
                        }
                        new OnKeyboardPressedEvent(letter).Publish(this);
                    }
                }
            }
        }

        public void SetTarget(Transform target)
        {
            if (target != null)
            {
                _target = target;
                _targetPosition = target.position;
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

            if (_target != null && _target.gameObject.activeInHierarchy)
            {
                _targetPosition = _target.position;
            }

            transform.position = Vector2.MoveTowards(
                transform.position,
                _targetPosition,
                _moveSpeed * Time.deltaTime
            );

            float distance = Vector2.Distance(transform.position, _targetPosition);
            if (distance <= 0.05f)
            {
                _hasTarget = false;
                _rigidbody.gravityScale = 3;
            }
        }

        [OnGameEvent]
        private void OnEnemyDamaged(OnDamageEnemyByLetterEvent e)
        {
            if (e.Enemy != null)
            {
                SetTarget(e.Enemy.transform);
            }
        }
    }
}
