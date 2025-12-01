using Code.Abstracts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    public class SkillOptionView : DraggableItem<SkillBase>
    {
        [SerializeField] private Image _skillImage;
        [SerializeField] private TextMeshProUGUI _skillName;
        [SerializeField] private TextMeshProUGUI _skillDescription;
        private SkillBase _skillBase;

        public override SkillBase Data() => _skillBase;

        public void Setup(SkillBase skillBase)
        {
            _skillBase = skillBase;
            _skillImage.sprite = skillBase.Sprite;
            _skillName.text = skillBase.SkillName;
            _skillDescription.text = skillBase.Description;
        }
    }
}