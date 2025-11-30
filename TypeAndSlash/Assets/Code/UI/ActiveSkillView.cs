using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    public class ActiveSkillView : MonoBehaviour
    {
        [SerializeField] private Image _skillImage;
        [SerializeField] private Image _coolDownImage;
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
            _coolDownImage.sprite = skillSprite;
        }

        public void UpdateCoolDownImage(float coolDownTimer)
        {
            _coolDownImage.fillAmount = coolDownTimer;
        }
    }
}