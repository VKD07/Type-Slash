using UnityEngine;
using System.Collections.Generic;

namespace Code
{
    [CreateAssetMenu(fileName = "Words", menuName = "SO/New Words")]
    public class Words : ScriptableObject
    {
        [SerializeField] private List<string> _defaultWords = new List<string>
        {
            "arch", "bolt", "chop", "dash", "ember", "flare", "glow",
            "heal", "iron", "jolt", "kick", "lure", "mend", "nail",
            "omen", "pier", "quip", "rush", "stab", "twin", "undo",
            "vent", "warp", "xeno", "yank", "zince"
        };

        [SerializeField] private List<string> _runtimeWords = new List<string>();

        private void OnEnable()
        {
            ResetToDefault();
        }

        public string GetRandomWord()
        {
            if (_runtimeWords.Count == 0)
            {
                ResetToDefault();
            }

            int index = Random.Range(0, _runtimeWords.Count);
            string word = _runtimeWords[index];
            _runtimeWords.RemoveAt(index);
            return word;
        }

        public void AddWord(string newWord)
        {
            if (string.IsNullOrWhiteSpace(newWord))
            {
                return;
            }

            if (!_runtimeWords.Contains(newWord))
            {
                _runtimeWords.Add(newWord);
            }
        }
        
        public void RemoveWordBasedoOnFirstLetter(char letter)
        {
            for (int i = 0; i < _runtimeWords.Count; i++)
            {
                if (_runtimeWords[i][0] == letter)
                {
                    _runtimeWords.RemoveAt(i);
                }
            }
        }

        private void ResetToDefault()
        {
            _runtimeWords = new List<string>(_defaultWords);
            RemoveDuplicates();
        }

        private void RemoveDuplicates()
        {
            HashSet<string> unique = new HashSet<string>(_runtimeWords);
            _runtimeWords = new List<string>(unique);
        }
    }
}