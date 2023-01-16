using GameStateMachine.GameStates;
using UI;
using UnityEngine;

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

    public class OnPlayerCollectedJewel
    {
        public Vector3 playerPosition;
        public Jewel.Jewel jewel;
        
        public OnPlayerCollectedJewel(Vector3 playerPosition, Jewel.Jewel jewel)
        {
            this.playerPosition = playerPosition;
            this.jewel = jewel;
        }
    }

    public class OnPlayerDeath { }
    
    public class OnPlayerChangedMoveDirection { }


    public class OnPlayerSkinSelected
    {
        public int skinId;

        public OnPlayerSkinSelected(int skinId)
        {
            this.skinId = skinId;
        }
    }
    
    
    public class OnPlayerSkinPurchased
    {
        public int skinId;

        public OnPlayerSkinPurchased(int skinId)
        {
            this.skinId = skinId;
        }
    }
    
}