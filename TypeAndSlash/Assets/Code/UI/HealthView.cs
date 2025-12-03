using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    public class HealthView : MonoBehaviour
    {
        [SerializeField] private Slider _healthSlider;

        public void Setup(float maxHealth)
        {
            _healthSlider.maxValue = maxHealth;
            _healthSlider.value = maxHealth;
        }

        public void UpdateValue(float val)
        {
            _healthSlider.value = val;
        }
    }
}