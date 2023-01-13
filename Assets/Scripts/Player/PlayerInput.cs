using System;
using Signals;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Player
{
    public class PlayerInput : MonoBehaviour
    {
        public event Action DirectionChanged;

        private bool _inputEnabled = false;
        
        private SignalBus _signalBus;
        
        [Inject]
        private void Init(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        private void Awake()
        {
            Input.simulateMouseWithTouches = true;
        }

        public void ChangeInputEnabledState(bool state)
        {
            _inputEnabled = state;
        }
        
        
        public void UpdateTick()
        {
            if (!_inputEnabled)
                return;
            
            if (!Input.GetMouseButtonDown(0))
                return;

            if (EventSystem.current.IsPointerOverGameObject())
                return;
            
            DirectionChanged?.Invoke();
        }
    }
}