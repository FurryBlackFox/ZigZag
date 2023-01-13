using GameStateMachine.GameStates;
using UI;

namespace Signals
{
    public class OnGameStateChanged
    {
        public GameStateType prevStateType;
        public GameStateType currentStateType;
        
        public OnGameStateChanged(GameStateType prevStateType, GameStateType currentStateType)
        {
            this.prevStateType = prevStateType;
            this.currentStateType = currentStateType;
        }
    }
    
    public class OnGameStateChangeButtonClick
    {
        public GameStateType buttonTargetType;

        public OnGameStateChangeButtonClick(GameStateType buttonTargetType)
        {
            this.buttonTargetType = buttonTargetType;
        }
    }

    public class OnPlayerCollectedJewel { }

    public class OnPlayerDeath { }
    
    public class OnPlayerChangedMoveDirection { }
}