using GameStateMachine.GameStates;
using UI;

namespace Signals
{
    public class OnGameStateChanged
    {
        public readonly GameStateType prevStateType;
        public readonly GameStateType currentStateType;
        
        public OnGameStateChanged(GameStateType prevStateType, GameStateType currentStateType)
        {
            this.prevStateType = prevStateType;
            this.currentStateType = currentStateType;
        }
    }
    
    public class OnGameStateChangeButtonClick
    {
        public readonly GameStateType buttonTargetType;

        public OnGameStateChangeButtonClick(GameStateType buttonTargetType)
        {
            this.buttonTargetType = buttonTargetType;
        }
    }

    public class OnPlayerCollectedJewel { }

    public class OnPlayerDeath { }
    
    public class OnPlayerChangedMoveDirection { }

    public class OnJewelDespawned
    {
        public readonly Jewel.Jewel jewel;

        public OnJewelDespawned(Jewel.Jewel jewel)
        {
            this.jewel = jewel;
        }
    }
}