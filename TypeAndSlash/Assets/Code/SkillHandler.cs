using System.Collections.Generic;
using Code.Abstracts;
using Code.GameEvents;
using Code.Interface;
using CriminalMakers.GameEventHub;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code
{
    public class SkillHandler : MonoBehaviour
    {
        [SerializeField] private List<SkillBase> startingSkills;
        [SerializeField] private ActiveSkillViewController _activeSkillViewController;
        private readonly List<SkillBase> _activeSkills = new();

        private SkillContext _context;


        private void OnEnable()
        {
            _context = new SkillContext
            {
                Player = this.gameObject,
                SkillHandler = this
            };

            foreach (SkillBase skill in startingSkills)
            {
                AddSkill(skill);
            }

            GameEventHub.Bind(this);
        }

        private void OnDestroy()
        {
            GameEventHub.Unbind(this);
        }

        private void Update()
        {
            float dt = Time.deltaTime;

            foreach (SkillBase skill in _activeSkills)
            {
                skill.Tick(dt);

                if (skill is ActiveSkillBase activeSkill)
                {
                    Key key = activeSkill.ActivationKey;
                    _activeSkillViewController.UpdateSkillCooldown(activeSkill.SkillName, activeSkill.GetNormalizedCooldownTimer);
                    if (key != Key.None && Keyboard.current[key].wasPressedThisFrame)
                    {
                        activeSkill.Activate();
                    }
                }
            }
        }


        public void AddSkill(SkillBase skillAsset)
        {
            skillAsset.Initialize(_context);
            skillAsset.OnAcquire();
            _activeSkills.Add(skillAsset);

            if (skillAsset is ActiveSkillBase activeSkill)
            {
                _activeSkillViewController.AddActiveSkillView(activeSkill.SkillName, activeSkill.Description, activeSkill.Sprite);
            }
        }

        public void RemoveSkill(SkillBase skillInstance)
        {
            skillInstance.OnRemove();
            _activeSkills.Remove(skillInstance);
        }

        public void TriggerActiveSkill(SkillBase skill)
        {
            skill.Activate();
        }

        [OnGameEvent]
        private void ActivatePassiveSkillOnEnemyDamaged(OnDamageEnemyByLetterEvent e)
        {
            foreach (SkillBase skill in _activeSkills)
            {
                if (skill is IActivateOnEnemyDamaged enemyDamagedSkill)
                {
                    enemyDamagedSkill.OnEnemyDamaged(e.Enemy);
                }
            }
        }

        [OnGameEvent]
        private void ActivatePassiveSkillOnEnemyKilled(OnEnemyKilledEvent e)
        {
            foreach (SkillBase skill in _activeSkills)
            {
                if (skill is IActivateOnEnemyKilled passiveSKill)
                {
                    passiveSKill.OnEnemyKilled(e.KilledEnemy);
                }
            }
        }

        [OnGameEvent]
        private void OnSuperSkillEvent(OnSuperSkillActive e)
        {
            foreach (SkillBase skill in _activeSkills)
            {
                if (skill is SuperSkillBase superSkillBase)
                {
                    if (e.IsActive)
                    {
                        superSkillBase.Activate();
                    }
                    else
                    {
                        superSkillBase.Deactivate();
                    }
                }
            }
        }
    }
}