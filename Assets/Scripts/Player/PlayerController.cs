using GameStateMachine.GameStates;
using Installers.GlobalManagers;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField, Required] private AiInput _aiInput;
        [SerializeField, Required] private PlayerInput _playerInput;
        [SerializeField, Required] private PlayerMovement _playerMovement;

        private SignalBus _signalBus;
        private SettingsManager _settingsManager;

        private AbstractInput _activeInput;
        
        [Inject]
        private void Init(SignalBus signalBus, SettingsManager settingsManager)
        {
            _signalBus = signalBus;
            _settingsManager = settingsManager;
            
            
            ChangeAiInputEnabledState(_settingsManager.AiInputSavableData.Value);

            _signalBus.Subscribe<OnGameStateChanged>(OnGameStateChanged);
            _signalBus.Subscribe<OnPlayerAiInputEnabledStateChanged>(OnPlayerAiInputEnableStateChanged);
        }

        private void OnDestroy()
        {
            _signalBus.Unsubscribe<OnGameStateChanged>(OnGameStateChanged);
            _signalBus.Unsubscribe<OnPlayerAiInputEnabledStateChanged>(OnPlayerAiInputEnableStateChanged);
            
            if(_activeInput)
                _activeInput.DirectionChanged -= _playerMovement.ChangeDirection;
        }

        private void OnValidate()
        {
            if (_aiInput == null)
                _aiInput = GetComponentInChildren<AiInput>();
            
            if (_playerInput == null)
                _playerInput = GetComponentInChildren<PlayerInput>();

            if (_playerMovement == null)
                _playerMovement = GetComponentInChildren<PlayerMovement>();
        }

        private void OnPlayerAiInputEnableStateChanged(OnPlayerAiInputEnabledStateChanged aiInputEnabledStateEvent)
        {
            ChangeAiInputEnabledState(aiInputEnabledStateEvent.state);
        }

        private void ChangeAiInputEnabledState(bool state)
        {
            if(_activeInput)
                _activeInput.DirectionChanged -= _playerMovement.ChangeDirection;

            _activeInput = state
                ? (AbstractInput)_aiInput
                : (AbstractInput)_playerInput;
            _activeInput.DirectionChanged += _playerMovement.ChangeDirection;

        }

        private void OnGameStateChanged(OnGameStateChanged stateChangedEvent)
        {
            switch (stateChangedEvent.currentStateType)
            {
                case GameStateType.MainMenu:
                    ChangeInputEnabledStates(false);
                    _playerMovement.ResetValues();
                    _playerMovement.ChangeMoveAvailabilityState(false);
                    _playerMovement.SetGravityEnabledState(false);
                    ResetInputValues();
                    break;
                case GameStateType.Play:
                    ChangeInputEnabledStates(true);
                    _playerMovement.ChangeMoveAvailabilityState(true);
                    break;
                case GameStateType.Pause:
                    ChangeInputEnabledStates(false);
                    _playerMovement.ChangeMoveAvailabilityState(false);
                    break;
                case GameStateType.Defeat:
                    ChangeInputEnabledStates(false);
                    _playerMovement.SetGravityEnabledState(true);
                    break;
            }
        }

        private void ChangeInputEnabledStates(bool state)
        {
            _aiInput.ChangeInputEnabledState(state);
            _playerInput.ChangeInputEnabledState(state);
        }

        private void ResetInputValues()
        {
            _aiInput.ResetValues();
            _playerInput.ResetValues();
        }
    }
}
