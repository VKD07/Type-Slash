using Code.GameEvents;
using Code.Interface;
using CriminalMakers.GameEventHub;
using TMPro;
using UnityEngine;

namespace Code
{
    public class Enemy : MonoBehaviour, IKnockable, IDamageable
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private float _moveSpeed = 10f;

        private EnemyKiller _enemyKiller;
        private string _assignedWord;
        private string _currentWord;
        private bool _isGrounded;
        private float _initMoveSpeed;

        private bool _hasChangedSpeed;

        public string AssignedWord => _assignedWord;
        public string CurrentWord => _currentWord;
        
        private void Awake()
        {
            _initMoveSpeed = _moveSpeed;
            GameEventHub.Bind(this);
        }

        private void OnDestroy()
        {
            GameEventHub.Unbind(this);
        }

        private void OnEnable()
        {
            _enemyKiller = FindAnyObjectByType<EnemyKiller>();
        }

        public void AssignAWord(string word)
        {
            _assignedWord = word;
            _currentWord = word;
            _text.text = _currentWord;
        }

        public void AssignNewWord(string newWord)
        {
            _currentWord = newWord;
            _text.text = _currentWord;
        }

        public void TakeDamageBasedOnLetter(char letter)
        {
            if (_currentWord.Length == 0)
            {
                return;
            }

            if (char.ToLowerInvariant(_currentWord[0]) != letter)
            {
                return;
            }

            TakeDamage(1);
        }

        private void Update()
        {
            if (_enemyKiller != null && _isGrounded)
            {
                transform.position = Vector2.MoveTowards(
                    transform.position,
                    _enemyKiller.transform.position,
                    _moveSpeed * Time.deltaTime
                );
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            _isGrounded = true;
        }

        [OnGameEvent]
        private void OnModifiedSpeedEvent(OnEnemyMoveSpeedModified e)
        {
            _hasChangedSpeed = false;
            if (e.Reset)
            {
                _moveSpeed = _initMoveSpeed;
                return;
            }
            
            ReduceMoveSpeed(e.NewMoveSpeed);
        }

        public void KnockBack(Vector3 dir, float strength)
        {
            transform.position += dir * strength;
        }

        public void TakeDamage(int damage)
        {
            int remove = Mathf.Min(damage, _currentWord.Length);
            _currentWord = _currentWord.Substring(remove);
            _text.text = _currentWord;

            if (_currentWord.Length == 0)
            {
                new OnEnemyKilledEvent(this).Publish(this);
                gameObject.SetActive(false);
            }
        }

        public void ReduceMoveSpeed(float val)
        {
            if (!_hasChangedSpeed)
            {
                _hasChangedSpeed = true;
                if (_moveSpeed - val <= 0)
                {
                    _moveSpeed = 0f;
                    return;
                }
                _moveSpeed -= val;
            }
        }
    }
}