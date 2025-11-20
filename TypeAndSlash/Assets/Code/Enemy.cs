using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace Code
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private Words _words;
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Transform _target;
        [SerializeField] private float _moveSpeed = 5.0f;
        private string _currentWord;

        private void OnEnable()
        {
            AssignAWord();
        }

        private void Update()
        {
            foreach (KeyControl key in Keyboard.current.allKeys)
            {
                if (key != null && key.wasPressedThisFrame)
                {
                    string keyName = key.displayName;

                    if (keyName.Length == 1)
                    {
                        char letter = keyName.ToLower()[0];
                        Damage(letter);
                    }
                }
            }
            
            // Move();
        }


        public void Damage(char letter)
        {
            if (_currentWord.Length > 0 && char.ToLowerInvariant(_currentWord[0]) == letter)
            {
                _currentWord = _currentWord.Substring(1);
                _text.text = _currentWord;

                if (_currentWord.Length == 0)
                {
                    gameObject.SetActive(false);
                }
            }
        }

        private void Move()
        {
            transform.position = Vector2.MoveTowards(transform.position, _target.position, _moveSpeed * Time.deltaTime);
        }

        private void AssignAWord()
        {
            _currentWord = _words.GetRandomWords();
            if (_currentWord != String.Empty)
            {
                _text.text = _currentWord;
            }
        }
    }
}