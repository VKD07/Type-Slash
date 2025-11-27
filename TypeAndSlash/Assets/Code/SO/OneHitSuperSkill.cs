using Code.Abstracts;
using Code.GameEvents;
using UnityEngine;

namespace Code.SO
{
    [CreateAssetMenu (fileName = "OneHitSuperSkill", menuName = "Skills/SuperSkills/OneHitSuperSkill")]
    public class OneHitSuperSkill : SuperSkillBase
    {
        [SerializeField] private string AssignedWord = "hyperblast";        
        protected override void OnSuperStart()
        {
            new OnOneHitSuperSkillEvent(true,AssignedWord).Publish(this);
        }

        protected override void OnSuperTick(float deltaTime, float duration)
        {
            
        }

        protected override void OnSuperEnd()
        {
            new OnOneHitSuperSkillEvent(false, AssignedWord).Publish(this);
        }
    }
}