using System.Collections.Generic;
using Code.Abstracts;
using Code.GameEvents;
using CriminalMakers.GameEventHub;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code.UI
{
    public class SkillOptionViewController : MonoBehaviour
    {
        [SerializeField] private List<SkillBase> _skills;
        [SerializeField] private SkillOptionView[] _skillOptionViews;
        [SerializeField] private GameObject _chooseSkillPanel;

        private void Awake()
        {
            GameEventHub.Bind(this);
            SetActiveOptions(false);
        }
        
        private void OnDestroy()
        {
            GameEventHub.Unbind(this);
        }

        public void SetActiveOptions(bool val)
        {
            foreach (SkillOptionView view in _skillOptionViews)
            {
                view.gameObject.SetActive(val);
            }
        }

        public void ShowSkillOptions()
        {
            if (_skills.Count < 3)
            {
                Debug.LogWarning("Not enough skills.");
                return;
            }

            List<SkillBase> temp = new List<SkillBase>(_skills);

            for (int i = 0; i < temp.Count; i++)
            {
                int rand = Random.Range(i, temp.Count);
                (temp[i], temp[rand]) = (temp[rand], temp[i]);
            }

            for (int i = 0; i < 3; i++)
            {
                _skillOptionViews[i].Setup(temp[i]);
                _skillOptionViews[i].gameObject.SetActive(true);
            }
        }

        private void Update()
        {
            if (Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                ShowSkillOptions();
            }
        }

        [OnGameEvent]
        private void OnChosenSkill(OnChosenSkillEvent e)
        {
            SetActiveOptions(false);
            _skills.Remove(e.SkillBase);
        }
    }
}