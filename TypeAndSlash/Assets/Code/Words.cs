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

        [SerializeField] private List<string> runtimeWords = new List<string>();

        private void OnEnable()
        {
            ResetToDefault();
        }

        public string GetRandomWord()
        {
            if (runtimeWords.Count == 0)
            {
                ResetToDefault();
            }

            int index = Random.Range(0, runtimeWords.Count);
            string word = runtimeWords[index];
            runtimeWords.RemoveAt(index);
            return word;
        }

        public void AddWord(string newWord)
        {
            if (string.IsNullOrWhiteSpace(newWord))
                return;

            if (!runtimeWords.Contains(newWord))
            {
                runtimeWords.Add(newWord);
            }
        }

        private void ResetToDefault()
        {
            runtimeWords = new List<string>(_defaultWords);
            RemoveDuplicates();
        }

        private void RemoveDuplicates()
        {
            HashSet<string> unique = new HashSet<string>(runtimeWords);
            runtimeWords = new List<string>(unique);
        }

        public bool Contains(string word)
        {
            return runtimeWords.Contains(word);
        }
    }
}