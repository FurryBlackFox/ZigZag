using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace GameStateMachine.GameStates
{
    //[Flags]
    public enum GameStateType
    {
        MainMenu = 1 << 0,
        Settings = 1 << 1,
        SkinShop = 1 << 2,
        Play = 1 << 3,
        Pause= 1 << 4,
        Defeat = 1 << 5,
    }
    
    public abstract class AbstractGameState
    {
        protected GameStateMachine gameStateMachine;
        protected SignalBus signalBus;

        public AbstractGameState(GameStateMachine gameStateMachine, SignalBus signalBus)
        {
            this.gameStateMachine = gameStateMachine;
            this.signalBus = signalBus;
        }

        public abstract UniTask Enter();

        public abstract UniTask Exit();
    }
}