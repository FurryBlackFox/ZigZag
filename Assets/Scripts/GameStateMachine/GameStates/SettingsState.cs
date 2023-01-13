using Cysharp.Threading.Tasks;
using Signals;
using Zenject;

namespace GameStateMachine.GameStates
{
    public class SettingsState : AbstractGameState
    {
        public SettingsState(GameStateMachine gameStateMachine, SignalBus signalBus) : base(gameStateMachine, signalBus)
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
            if (buttonClickEvent.buttonTargetType == GameStateType.MainMenu)
            {
                gameStateMachine.ChangeState(buttonClickEvent.buttonTargetType);
            }
        }
    }
}