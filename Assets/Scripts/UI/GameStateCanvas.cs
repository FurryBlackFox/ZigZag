using System.Collections.Generic;
using Cysharp.Threading.Tasks;
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
        [SerializeField, Required] private CanvasGroup _canvasGroup;
        [SerializeField] private GameStateCanvasAnimator _stateAnimator;

        private SignalBus _signalBus;

        private bool _currentEnabledState;

        private void OnValidate()
        {
            if (_canvas == null)
                _canvas = GetComponentInChildren<Canvas>();

            if (_canvasGroup == null)
                _canvasGroup = GetComponentInChildren<CanvasGroup>();
            
            if (_stateAnimator == null)
                _stateAnimator = GetComponentInChildren<GameStateCanvasAnimator>();
        }

        [Inject]
        private async void Init(SignalBus signalBus)
        {
            _signalBus = signalBus;
            
            _signalBus.Subscribe<OnGameStateChanged>(OnGameStateChanged);
            
            await TryToChangeState(false, true);
        }

        private void OnDestroy()
        {
            _signalBus.Unsubscribe<OnGameStateChanged>(OnGameStateChanged);
        }

        private async void OnGameStateChanged(OnGameStateChanged onGameStateChangedEvent)
        {
            var currentGameStateType = onGameStateChangedEvent.currentStateType;
            var newState = _enabledOnGameStateTypesList.Contains(currentGameStateType);

            await TryToChangeState(newState);
        }

        private async UniTask TryToChangeState(bool newState, bool force = false)
        {
            if (_currentEnabledState == newState && !force)
                return;

            _currentEnabledState = newState;
            
            _canvasGroup.interactable = _currentEnabledState;
            _canvasGroup.blocksRaycasts = _currentEnabledState;

            if (!_currentEnabledState)
                await TryToPlayStateChangedAnimation(false, force);

            _canvas.enabled = _currentEnabledState;

            if (_currentEnabledState)
                await TryToPlayStateChangedAnimation(true, force);
        }

        private async UniTask TryToPlayStateChangedAnimation(bool newState, bool instant)
        {
            if(!_stateAnimator)
                return;

            if (!newState)
            {
                if (instant)
                    _stateAnimator.InstantHidePanels();
                else
                    await _stateAnimator.HidePanels();
                
                return;
            }

            if (instant)
                _stateAnimator.InstantShowPanels();
            else
                await _stateAnimator.ShowPanels();
        }
    }
}