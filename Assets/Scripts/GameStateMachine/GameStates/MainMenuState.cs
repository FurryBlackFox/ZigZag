using System;
using Cysharp.Threading.Tasks;
using Signals;
using UI;
using UnityEngine;
using Zenject;

namespace GameStateMachine.GameStates
{
    public class MainMenuState : AbstractGameState
    {
        public MainMenuState(GameStateMachine gameStateMachine, SignalBus signalBus) : base(gameStateMachine, signalBus)
        {
        }

        public async override UniTask Enter()
        {
            signalBus.Subscribe<OnGameStateChangeButtonClick>(OnGameStateChangeButtonClick);
        }

        public async override UniTask Exit()
        {
            signalBus.Unsubscribe<OnGameStateChangeButtonClick>(OnGameStateChangeButtonClick);
        }

        private void OnGameStateChangeButtonClick(OnGameStateChangeButtonClick buttonClickEvent)
        {
            switch (buttonClickEvent.buttonTargetType)
            {
                case GameStateType.Play:
                case GameStateType.Settings:
                case GameStateType.SkinShop:
                    gameStateMachine.ChangeState(buttonClickEvent.buttonTargetType);
                    break;
            }
        }
    }
}