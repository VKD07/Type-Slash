using Code.GameEvents;
using CriminalMakers.GameEventHub;
using TMPro;
using UnityEngine;

namespace Code.UI
{
    public class TypedWordView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _typedText;
        [SerializeField] private TextMeshProUGUI _targetWord;

        private void Awake()
        {
            GameEventHub.Bind(this);
            _typedText.text = string.Empty;
            _targetWord.text = string.Empty;
        }

        private void OnDestroy()
        {
            GameEventHub.Unbind(this);
        }

        [OnGameEvent]
        public void SetTargetWord(OnDamageEnemyByLetterEvent e)
        {
            string newWord = e.Enemy.AssignedWord;
            if (_targetWord.text != newWord)
            {
                _typedText.text = string.Empty;
            }

            _targetWord.text = newWord;
        }

        [OnGameEvent]
        public void UpdateTypedText(OnKeyboardPressedEvent e)
        {
            if (_targetWord.text.Length == 0)
            {
                return;
            }

            int index = _typedText.text.Length;

            if (index >= _targetWord.text.Length)
            {
                return;
            }

            if (char.ToLowerInvariant(_targetWord.text[index]) == e.KeyChar)
            {
                _typedText.text += e.KeyChar;
            }
        }

        [OnGameEvent]
        public void ClearText(OnEnemyKilledEvent e)
        {
            if (e.KilledEnemy.AssignedWord == _targetWord.text)
            {
                _typedText.text = string.Empty;
                _targetWord.text = string.Empty;
            }
        }

        [OnGameEvent]
        private void ClearText(OnSuperSkillActive e)
        {
            _typedText.text = string.Empty;
            _targetWord.text = string.Empty;
        }
    }
}