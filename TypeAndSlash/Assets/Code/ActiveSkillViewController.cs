using Code.UI;
using UnityEngine;

namespace Code
{
    public class ActiveSkillViewController : MonoBehaviour
    {
        [SerializeField] private ActiveSkillView[] _activeSkillViews;
        
        public void AddActiveSkillView(string skillName, string description, Sprite skillSprite)
        {
            foreach (ActiveSkillView activeSkillView in _activeSkillViews)
            {
                activeSkillView.Setup(skillName, description, skillSprite);
                break;
            }
        }

        public void UpdateSkillCooldown(string skillName, float coolDownTimer)
        {
            foreach (ActiveSkillView _activeSkill in _activeSkillViews)
            {
                if (skillName == _activeSkill.Name)
                {
                    _activeSkill.UpdateCoolDownImage(coolDownTimer);
                }
            }
        }
    }
}