using Installers.GlobalManagers;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using Utils.SavableData;
using Zenject;

namespace UI
{
    public class SettingsToggleButton : MonoBehaviour
    {
        public enum SettingsControlType
        {
            None = 0,
            Music = 1,
            PlayerAiInput = 2,
        }
        
        [SerializeField, Required] private Toggle _toggle;
        [SerializeField] private SettingsControlType _controlType = SettingsControlType.None;

        private SignalBus _signalBus;
        private SettingsManager _settingsManager;

        private BooleanSavableData _targetBooleanSavableData;
        
        [Inject]
        private void Init(SignalBus signalBus, SettingsManager settingsManager)
        {
            _signalBus = signalBus;
            _settingsManager = settingsManager;

            _targetBooleanSavableData = GetTargetBooleanData();

            UpdateToggleValue();
            _targetBooleanSavableData.OnValueChanged += UpdateToggleValue;
            _toggle.onValueChanged.AddListener(OnToggleValueChanged);
        }

        private void OnDestroy()
        {
            _targetBooleanSavableData.OnValueChanged -= UpdateToggleValue;
            _toggle.onValueChanged.RemoveListener(OnToggleValueChanged);
        }

        private void OnValidate()
        {
            if (_toggle == null)
                _toggle = GetComponentInChildren<Toggle>();
        }

        private void UpdateToggleValue()
        {
            _toggle.isOn = _targetBooleanSavableData.Value;
        }

        private void OnToggleValueChanged(bool state)
        {
            _targetBooleanSavableData.SetValue(state);
        }
        
        private BooleanSavableData GetTargetBooleanData()
        {
            return _controlType switch
            {
                SettingsControlType.Music => _settingsManager.MusicEnabledSavableData,
                SettingsControlType.PlayerAiInput => _settingsManager.AiInputSavableData,
                _ => null
            };
        }
    }
}