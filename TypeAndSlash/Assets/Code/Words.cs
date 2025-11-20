using UnityEngine;

namespace Code
{
    [CreateAssetMenu(fileName = "Words", menuName = "SO/New Words")]
    public class Words : ScriptableObject
    {
        [SerializeField] private string [] words;

        public string GetRandomWords()
        {
            return words[Random.Range(0, words.Length)];
        }
    }
}