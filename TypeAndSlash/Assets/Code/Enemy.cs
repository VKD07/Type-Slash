using System;
using TMPro;
using UnityEngine;

namespace Code
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private SlashController _slashController;
        [SerializeField] private float _moveSpeed = 10f;
        private EnemyKiller _enemyKiller;
        
        public string CurrentWord => _currentWord;
        public event Action<Enemy> OnWordFinished;
        public event Action<Enemy> OnDeath;

        private string _currentWord;

        private bool _isGrounded;

        private void OnEnable()
        {
            if (_slashController == null)
                _slashController = FindAnyObjectByType<SlashController>();

            _enemyKiller = FindAnyObjectByType<EnemyKiller>();
        }

        public void AssignAWord(string word)
        {
            _currentWord = word;
            _text.text = _currentWord;
        }

        public void Damage(char letter)
        {
            if (_currentWord.Length == 0) return;
            if (char.ToLowerInvariant(_currentWord[0]) != letter) return;

            _currentWord = _currentWord.Substring(1);
            _text.text = _currentWord;
            _slashController.SetTransform(transform.position);

            if (_currentWord.Length == 0)
            {
                OnWordFinished?.Invoke(this);
                OnDeath?.Invoke(this);
                gameObject.SetActive(false);
            }
        }

        private void Update()
        {
            GoToTarget();
        }

        private void GoToTarget()
        {
            if (_enemyKiller != null && _isGrounded)
            {
                transform.position = Vector2.MoveTowards(transform.position, _enemyKiller.transform.position, _moveSpeed * Time.deltaTime);
            }
        }

        public void KnockBackUp(float knockBackStr)
        {
            transform.position += Vector3.up * knockBackStr;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            _isGrounded = true;
        }
    }
}