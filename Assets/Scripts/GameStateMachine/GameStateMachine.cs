using System;
using System.Collections.Generic;
using GameStateMachine.GameStates;
using Signals;
using UnityEngine;
using Zenject;

namespace GameStateMachine
{
    public class GameStateMachine : MonoBehaviour
    {
        protected Dictionary<GameStateType, AbstractGameState> _gameStatesDictionary;
        
        private SignalBus _signalBus;
        
        private AbstractGameState _currentState;
        
        [Inject]
        private void Init(SignalBus signalBus)
        {
            _signalBus = signalBus;
            
            InitStates();
        }


        private void InitStates()
        {
            _gameStatesDictionary = new Dictionary<GameStateType, AbstractGameState>
            {
                //{ typeof(BoostrapState), new BoostrapState(this) },
                { GameStateType.MainMenu, new MainMenuState(this, _signalBus) },
                { GameStateType.Settings, new SettingsState(this, _signalBus) },
                { GameStateType.SkinShop, new SkinShopState(this, _signalBus) },
                { GameStateType.Play, new PlayState(this, _signalBus) },
                { GameStateType.Pause, new PauseState(this, _signalBus) },
                { GameStateType.Defeat, new DefeatState(this, _signalBus) },
            };
        }

        public async void ChangeState(GameStateType newStateType)
        {
            if (_currentState != null)
                await _currentState.Exit();

            _currentState = _gameStatesDictionary[newStateType];
            await _currentState.Enter();
            
            _signalBus.Fire(new OnGameStateChanged(_currentState, newStateType));
        }

        private void Start()
        {
            ChangeState(GameStateType.MainMenu);
        }
    }
}