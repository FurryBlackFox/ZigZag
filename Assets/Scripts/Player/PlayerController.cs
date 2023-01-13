using System;
using GameStateMachine.GameStates;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField, Required] private PlayerInput _playerInput;
        [SerializeField, Required] private PlayerMovement _playerMovement;

        private SignalBus _signalBus;
        
        [Inject]
        private void Init(SignalBus signalBus)
        {
            _signalBus = signalBus;
            
            _signalBus.Subscribe<OnGameStateChanged>(OnGameStateChanged);

            _playerInput.DirectionChanged += _playerMovement.ChangeDirection;
        }

        private void OnDestroy()
        {
            _signalBus.Unsubscribe<OnGameStateChanged>(OnGameStateChanged);
            
            _playerInput.DirectionChanged -= _playerMovement.ChangeDirection;
        }

        private void OnValidate()
        {
            if (_playerInput == null)
                _playerInput = GetComponentInChildren<PlayerInput>();

            if (_playerMovement == null)
                _playerMovement = GetComponentInChildren<PlayerMovement>();
        }

        private void Update()
        {
            _playerInput.UpdateTick();
        }

        private void OnGameStateChanged(OnGameStateChanged stateChangedEvent)
        {
            switch (stateChangedEvent.currentStateType)
            {
                case GameStateType.MainMenu:
                    _playerInput.ChangeInputEnabledState(false);
                    _playerMovement.ChangeMoveAvailabilityState(false);
                    _playerMovement.ResetValues();
                    break;
                case GameStateType.Play:
                    _playerInput.ChangeInputEnabledState(true);
                    _playerMovement.ChangeMoveAvailabilityState(true);
                    break;
                case GameStateType.Pause:
                    _playerInput.ChangeInputEnabledState(false);
                    _playerMovement.ChangeMoveAvailabilityState(false);
                    break;
                case GameStateType.Defeat:
                    _playerInput.ChangeInputEnabledState(false);
                    break;
            }
        }
    }
}
