using GameStateMachine.GameStates;
using UI;

namespace Signals
{
    public class OnGameStateChanged
    {
        public AbstractGameState abstractGameState;
        public GameStateType gameStateType;
        
        public OnGameStateChanged(AbstractGameState abstractGameState, GameStateType gameStateType)
        {
            this.abstractGameState = abstractGameState;
            this.gameStateType = gameStateType;
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
    
    
    public class OnPlayStateStart { }

    public class OnPlayerCollectedJewel { }

    public class OnPlayerDeath { }
    
    public class OnPlayerChangedMoveDirection { }
}