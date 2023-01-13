using Cysharp.Threading.Tasks;
using Zenject;

namespace GameStateMachine.GameStates
{
    public class BoostrapState : AbstractGameState
    {
        public BoostrapState(GameStateMachine gameStateMachine, SignalBus signalBus) : base(gameStateMachine, signalBus)
        {
        }

        public override UniTask Enter()
        {
            throw new System.NotImplementedException();
        }

        public override UniTask Exit()
        {
            throw new System.NotImplementedException();
        }
    }
}