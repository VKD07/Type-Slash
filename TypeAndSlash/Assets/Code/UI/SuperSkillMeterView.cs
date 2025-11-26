using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    public class SuperSkillMeterView : MonoBehaviour
    {
        [SerializeField] private Slider _slider;

        public void SetMaxValue(int maxValue)
        {
            _slider.maxValue = maxValue;
        }

        public void UpdateValue(float value)
        {
            _slider.value = value;
        }
    }
}