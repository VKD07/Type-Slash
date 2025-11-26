using Code.GameEvents;
using Code.UI;
using CriminalMakers.GameEventHub;
using UnityEngine;

namespace Code
{
    public class SuperSkillHandler : MonoBehaviour
    {
        [SerializeField] SuperSkillMeterView _superSkillMeterView;
        [SerializeField] private float _maxNumberToTrigger = 10f;
        [SerializeField] private float _gainPerHit = 0.3f;
        [SerializeField] private float _decreaseRate = 0.05f;

        private float _currentMeterVal;
        private void Awake()
        {
            InitSliderView();
            GameEventHub.Bind(this);
        }
        
        private void OnDestroy()
        {
            GameEventHub.Unbind(this);
        }

        private void Update()
        {
            DecreaseMeter();
        }

        private void DecreaseMeter()
        {
            if (_currentMeterVal > 0)
            {
                _currentMeterVal -= _decreaseRate * Time.deltaTime;
                _superSkillMeterView.UpdateValue(_currentMeterVal);
            }
        }


        private void InitSliderView()
        {
            _superSkillMeterView.SetMaxValue((int)_maxNumberToTrigger);
        }

        [OnGameEvent]
        private void IncreaseMeter(OnDamageEnemyEvent e)
        {
            if (_currentMeterVal < _maxNumberToTrigger)
            {
                _currentMeterVal += _gainPerHit;
                _superSkillMeterView.UpdateValue(_currentMeterVal);
                return;
            }
            new OnSuperSkillActive().Publish(this);
            _currentMeterVal = 0;
        }
    }
}