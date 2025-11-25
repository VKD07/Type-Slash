using UnityEngine;
using System.Collections.Generic;

namespace Code
{
    [CreateAssetMenu(fileName = "Words", menuName = "SO/New Words")]
    public class Words : ScriptableObject
    {
        [SerializeField] private List<string> defaultWords = new List<string>
        {
            "dash", "jump", "zap",  "cut",  "hit",
            "run",  "pop",  "slam", "rush", "bolt",
            "fire", "wind", "fury", "rage", "claw",
            "bite", "glow", "bash", "kick", "stab",
            "peek", "zoom", "whip", "warp", "snap",
            "spin", "burn", "zap",  "tiny", "lift"
        };

        [SerializeField] private List<string> runtimeWords = new List<string>();


        private void OnEnable()
        {
            if (runtimeWords == null || runtimeWords.Count == 0)
            {
                ResetToDefault();
            }
        }


        public string GetRandomWord()
        {
            if (runtimeWords.Count == 0)
            {
                ResetToDefault();
            }

            int index = Random.Range(0, runtimeWords.Count);
            string word = runtimeWords[index];

            runtimeWords.RemoveAt(index); // remove chosen word

            return word;
        }


        public void AddWord(string newWord)
        {
            if (!string.IsNullOrWhiteSpace(newWord))
            {
                runtimeWords.Add(newWord);
            }
        }


        private void ResetToDefault()
        {
            runtimeWords = new List<string>(defaultWords);
        }
    }
}