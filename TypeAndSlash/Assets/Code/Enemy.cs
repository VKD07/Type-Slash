using Code.Abstracts;
using Code.GameEvents;
using Code.Interface;
using CriminalMakers.GameEventHub;
using TMPro;
using UnityEngine;

namespace Code
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Enemy : Damageable, IKnockable, IDamageDealer
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private float _moveSpeed = 10f;
        private Rigidbody2D _rb;
        private Base _base;
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
            _rb = GetComponent<Rigidbody2D>();
        }

        private void OnDestroy()
        {
            GameEventHub.Unbind(this);
        }

        private void OnEnable()
        {
            _base = FindAnyObjectByType<Base>();
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

        private void FixedUpdate()
        {
            if (_base != null && _isGrounded)
            {
                Vector2 direction = (_base.transform.position - transform.position).normalized;
                _rb.MovePosition(_rb.position + direction * _moveSpeed * Time.fixedDeltaTime);
            }
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            if (other.gameObject.GetComponent<Enemy>() != null)
            {
                return;
            }

            _isGrounded = true;
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            _isGrounded = false;
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
            _rb.angularVelocity = 0;
            _isGrounded = false;
            _rb.AddForce(dir * strength, ForceMode2D.Impulse);
        }

        public void Move(Vector3 dir, float strength)
        {
            transform.position += dir * strength;
        }

        public override void TakeDamage(float damage)
        {
            int remove = Mathf.Min((int)damage, _currentWord.Length);
            _currentWord = _currentWord.Substring(remove);
            _text.text = _currentWord;

            if (_currentWord.Length == 0)
            {
                new OnEnemyKilledEvent(this).Publish(this);
                Die();
            }
        }

        protected override void Die()
        {
            gameObject.SetActive(false);
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

        public float Damage => 10;
    }
}