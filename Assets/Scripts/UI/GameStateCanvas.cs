using System;
using System.Collections.Generic;
using GameStateMachine.GameStates;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace UI
{
    public class GameStateCanvas : MonoBehaviour
    {
        [SerializeField, Required] private List<GameStateType> _enabledOnGameStateTypesList;

        [SerializeField, Required] private Canvas _canvas;

        private SignalBus _signalBus;

        private bool _currentEnabledState;

        private void OnValidate()
        {
            if (_canvas == null)
                _canvas = GetComponentInChildren<Canvas>();
        }

        [Inject]
        private void Init(SignalBus signalBus)
        {
            _signalBus = signalBus;
            
            _signalBus.Subscribe<OnGameStateChanged>(OnGameStateChanged);
            
            TryToChangeState(false, true);
        }

        private void OnDestroy()
        {
            _signalBus.Unsubscribe<OnGameStateChanged>(OnGameStateChanged);
        }

        private void OnGameStateChanged(OnGameStateChanged onGameStateChangedEvent)
        {
            var currentGameStateType = onGameStateChangedEvent.gameStateType;
            
            if (_enabledOnGameStateTypesList.Contains(currentGameStateType))
            {
                TryToChangeState(true);
                return;
            }

            TryToChangeState(false);
        }

        private void TryToChangeState(bool newState, bool force = false)
        {
            if (_currentEnabledState == newState && !force)
                return;

            _currentEnabledState = newState;

            _canvas.enabled = _currentEnabledState;
        }
    }
}