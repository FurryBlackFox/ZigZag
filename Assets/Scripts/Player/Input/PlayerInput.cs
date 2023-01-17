using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Player
{
    public class PlayerInput : AbstractInput
    {
        public override event Action DirectionChanged;

        private void Awake()
        {
            Input.simulateMouseWithTouches = true;
        }

        private void Update()
        {
            if (!inputEnabled)
                return;
            
            if (!Input.GetMouseButtonDown(0))
                return;

            if (EventSystem.current.IsPointerOverGameObject())
                return;
            
            DirectionChanged?.Invoke();
        }

        public override void ResetValues()
        {
        }
    }
}