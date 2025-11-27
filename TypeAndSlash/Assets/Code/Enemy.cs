using Code.GameEvents;
using Code.Interface;
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
        

        public string AssignedWord => _assignedWord;
        public string CurrentWord => _currentWord;

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
    }
}
