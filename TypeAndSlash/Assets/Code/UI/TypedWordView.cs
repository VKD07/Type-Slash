using TMPro;
using UnityEngine;

namespace Code.UI
{
    public class TypedWordView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _typedText;
        [SerializeField] private TextMeshProUGUI _targetWord;
        [SerializeField] private Transform _wordPanel;

        private void Awake()
        {
            _typedText.text = string.Empty;
            _targetWord.text = string.Empty;
        }

        public void SetTargetWord(string word)
        {
            _targetWord.text = word;
            _typedText.text = string.Empty;
        }

        public void UpdateTypedText(char typedChar)
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

            if (char.ToLowerInvariant(_targetWord.text[index]) == char.ToLowerInvariant(typedChar))
            {
                _typedText.text += typedChar;
            }
        }

        public void SetParent(Transform parent)
        {
            _wordPanel.SetParent(parent);
            _wordPanel.localPosition = new Vector3(0.43f, 0.73f, 0f);
            _wordPanel.gameObject.SetActive(true);
        }
    }
}