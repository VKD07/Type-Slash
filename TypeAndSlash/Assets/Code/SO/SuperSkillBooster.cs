using Code.Abstracts;
using UnityEngine;

namespace Code.SO
{
    [CreateAssetMenu(menuName = "Skills/Passive/SuperSkillBooster", fileName = "SuperSkillBoosterPassive")]
    public class SuperSkillBooster : PassiveSkillBase
    {
        [SerializeField] private float _gainRate = 0.5f;
        private SuperSkillHandler _superSkillHandler;

        public override void OnAcquire()
        {
            _superSkillHandler = _context.Player.GetComponent<SuperSkillHandler>();
            _superSkillHandler.IncreaseGainPerHit(_gainRate);
        }
    }
}