using System;
using UnityEngine;

namespace Player
{
    public class PlayerInput : MonoBehaviour
    {
        public event Action DirectionChanged;
        
        //public bool NeedToChangeDirection { get; set; }

        private bool _inputEnabled = true;

        private void Awake()
        {
            Input.simulateMouseWithTouches = true;
        }

        public void Enable()
        {
            _inputEnabled = true;
        }
        
        public void Stop()
        {
            _inputEnabled = false;
        }
        
        public void UpdateTick()
        {
            if (!_inputEnabled)
                return;
            
            if (!Input.GetMouseButtonDown(0))
                return;
            
            DirectionChanged?.Invoke();
        }
    }
}