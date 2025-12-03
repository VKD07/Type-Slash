using UnityEngine;
using System.Collections.Generic;

namespace Code
{
    [CreateAssetMenu(fileName = "Words", menuName = "SO/New Words")]
    public class Words : ScriptableObject
    {
        [SerializeField] private TextAsset _jsonFile;
        [SerializeField] private List<string> _runtimeWords = new List<string>();

        [System.Serializable]
        private class WordList
        {
            public string[] words;
        }

        private void OnEnable()
        {
            LoadJSON();
        }

        private void LoadJSON()
        {
            _runtimeWords.Clear();

            if (_jsonFile == null)
            {
                return;
            }

            WordList list = JsonUtility.FromJson<WordList>(_jsonFile.text);

            if (list == null || list.words == null)
            {
                return;
            }

            _runtimeWords = new List<string>(list.words);
            RemoveDuplicates();
        }

        public string GetRandomWord()
        {
            if (_runtimeWords.Count == 0)
            {
                LoadJSON();
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
            for (int i = _runtimeWords.Count - 1; i >= 0; i--)
            {
                if (_runtimeWords[i][0] == letter)
                {
                    _runtimeWords.RemoveAt(i);
                }
            }
        }

        private void RemoveDuplicates()
        {
            HashSet<string> unique = new HashSet<string>(_runtimeWords);
            _runtimeWords = new List<string>(unique);
        }
    }
}
