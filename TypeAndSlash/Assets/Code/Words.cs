using UnityEngine;
using System.Collections.Generic;

namespace Code
{
    [CreateAssetMenu(fileName = "Words", menuName = "SO/New Words")]
    public class Words : ScriptableObject
    {
        [SerializeField] private List<string> _defaultWords = new List<string>
        {
            "arch", // A
            "bolt", // B
            "chop", // C
            "dash", // D
            "ember",// E (if you need EXACT 4 letters, use "echo")
            "flare",// F (4 letters alt: "fend")
            "glow", // G
            "heal", // H
            "iron", // I
            "jolt", // J
            "kick", // K
            "lure", // L
            "mend", // M
            "nail", // N
            "omen", // O
            "pier", // P
            "quip", // Q
            "rush", // R
            "stab", // S
            "twin", // T
            "undo", // U
            "vent", // V
            "warp", // W
            "xeno", // X (valid game-friendly word)
            "yank", // Y
            "zince"  // Z (or "zinc" if you want true dictionary)
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
            runtimeWords = new List<string>(_defaultWords);
        }
    }
}