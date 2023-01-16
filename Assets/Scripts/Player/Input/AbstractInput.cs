using System;
using UnityEngine;
using Zenject;

namespace Player
{
    public abstract class AbstractInput : MonoBehaviour
    {
        public abstract event Action DirectionChanged;
        
        protected SignalBus signalBus;
        
        protected bool inputEnabled = false;
        
        [Inject]
        private void Init(SignalBus signalBus)
        {
            this.signalBus = signalBus;
        }

        public void ChangeInputEnabledState(bool state)
        {
            inputEnabled = state;
        }

        public abstract void ResetValues();
    }
}