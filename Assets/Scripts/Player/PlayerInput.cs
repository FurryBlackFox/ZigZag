using System;
using UnityEngine;

namespace Player
{
    public class PlayerInput : MonoBehaviour
    {
        public event Action DirectionChanged;
        
        //public bool NeedToChangeDirection { get; set; }
        
        
        public void UpdateTick()
        {
            Input.simulateMouseWithTouches = true;

            if (!Input.GetMouseButtonDown(0))
                return;
            
            DirectionChanged?.Invoke();
        }
    }
}