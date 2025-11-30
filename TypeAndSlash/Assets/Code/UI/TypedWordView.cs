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
        }

        private void OnDestroy()
        {
            GameEventHub.Unbind(this);
        }
        
        [OnGameEvent]
        public void SetTargetWord(OnDamageEnemyByLetterEvent e)
        {
            if (e.Enemy.AssignedWord != _targetWord.text)
            {
                _typedText.text = "";
                _typedText.text = e.Enemy.AssignedWord[0].ToString();
            }
            _targetWord.text = e.Enemy.AssignedWord;
        }
        
        [OnGameEvent]
        public void UpdateTypedText(OnKeyboardPressedEvent e)
        {
            _typedText.text += e.KeyChar.ToString();
        }

        [OnGameEvent]
        public void ClearText(OnEnemyKilledEvent e)
        {
            _typedText.text = "";
            _targetWord.text = "";
        }
        
        [OnGameEvent]
        public void DeleteLetter(OnIncorrectLetterPressed e)
        {
            _typedText.text = _typedText.text.Substring(0, _typedText.text.Length - 1);
        }
    }
}