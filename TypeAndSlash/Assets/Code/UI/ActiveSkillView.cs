using Code.Abstracts;
using Code.GameEvents;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Code.UI
{
    public class ActiveSkillView : DropZone
    {
        [SerializeField] private Image _skillImage;
        [SerializeField] private Image _coolDownImage;
        [SerializeField] private int _slotIndex;
        [SerializeField] private Key _activationKey;
        private string _skillName;
        private string _description;

        public string Name => _skillName;
        public string Description => _description;

        private void Awake()
        {
            _coolDownImage.fillAmount = 0;
        }

        public void Setup(string skillName, string description, Sprite skillSprite)
        {
            _skillName = skillName;
            _description = description;
            _skillImage.sprite = skillSprite;
        }

        public void UpdateCoolDownImage(float coolDownTimer)
        {
            _coolDownImage.fillAmount = coolDownTimer;
        }

        public override void OnDrop(PointerEventData eventData)
        {
            GameObject data = eventData.pointerDrag;
            if (data != null && data.TryGetComponent(out IDraggableItem draggableItem))
            {
                if (draggableItem.GetData() is SkillBase skill)
                {
                    skill.ActivationKey = _activationKey;
                    Setup(skill.SkillName, skill.Description, skill.Sprite);
                    new OnChosenSkillEvent(skill, _slotIndex).Publish(this);
                }
            }
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            //Implement effect
        }
    }
}