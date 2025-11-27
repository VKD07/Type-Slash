using Code.GameEvents;
using Code.UI;
using CriminalMakers.GameEventHub;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Code
{
    public class SuperSkillHandler : MonoBehaviour
    {
        [SerializeField] SuperSkillMeterView _superSkillMeterView;
        [SerializeField] private float _maxNumberToTrigger = 10f;
        [SerializeField] private float _gainPerHit = 0.3f;
        [SerializeField] private float _decreaseRate = 0.05f;
        [SerializeField] private float _superSkillDuration = 5f;
        
        private float _currentMeterVal;
        private bool _superActive;
        private float _superTimer;
        private bool _canBeActivated;

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
            if (_canBeActivated && Keyboard.current.backspaceKey.wasPressedThisFrame)
            {
                new OnSuperSkillActive(true).Publish(this);
                _superActive = true;
                _canBeActivated = false;
            }
            
            if (_superActive)
            {
                _superTimer += Time.deltaTime;
                float t = 1f - (_superTimer / _superSkillDuration);
                _currentMeterVal = Mathf.Max(0f, t * _maxNumberToTrigger);
                _superSkillMeterView.UpdateValue(_currentMeterVal);
                
                if (_superTimer >= _superSkillDuration)
                {
                    _superActive = false;
                    _currentMeterVal = 0f;
                    _superSkillMeterView.UpdateValue(0f);
                    new OnSuperSkillActive(false).Publish(this);
                }

                return;
            }

            DecreaseMeter();
        }

        private void DecreaseMeter()
        {
            if (_superActive || _canBeActivated)
            {
                return;
            }
            if (_currentMeterVal > 0f)
            {
                _currentMeterVal -= _decreaseRate * Time.deltaTime;
                if (_currentMeterVal < 0f) _currentMeterVal = 0f;
                _superSkillMeterView.UpdateValue(_currentMeterVal);
            }
        }

        private void InitSliderView()
        {
            _superSkillMeterView.SetMaxValue((int)_maxNumberToTrigger);
        }

        [OnGameEvent]
        private void IncreaseMeter(OnDamageEnemyByLetterEvent e)
        {
            if (_superActive || _canBeActivated)
            {
                return;
            }

            _currentMeterVal += _gainPerHit;
            
            if (_currentMeterVal >= _maxNumberToTrigger)
            {
                _currentMeterVal = _maxNumberToTrigger;
                _superTimer = 0f;
                _canBeActivated = true;
            }

            _superSkillMeterView.UpdateValue(_currentMeterVal);
        }
    }
}
