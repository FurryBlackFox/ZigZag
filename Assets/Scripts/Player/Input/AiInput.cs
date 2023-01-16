using System;
using Settings;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Player
{
    public class AiInput : AbstractInput
    {
        public override event Action DirectionChanged;
 

        private PlayerSettings _playerSettings;

        private PlayerMovement.Direction _currentDirection;

        [Inject]
        private void InitPlayerSettings(PlayerSettings playerSettings)
        {
            _playerSettings = playerSettings;

            ResetValues();
            
            signalBus.Subscribe<OnPlayerCollidedAiDirectionTrigger>(OnPlayerCollidedAiDirectionTrigger);
        }

        private void OnDestroy()
        {
            signalBus.Unsubscribe<OnPlayerCollidedAiDirectionTrigger>(OnPlayerCollidedAiDirectionTrigger);
        }

        private void OnPlayerCollidedAiDirectionTrigger(OnPlayerCollidedAiDirectionTrigger collidedTriggerEvent)
        {
            if (!inputEnabled)
                return;
            
            var directionForward = collidedTriggerEvent.aiDirection.transform.forward;
            var dotValue = Vector3.Dot(directionForward, Vector3.right);
            
            var newDirection = dotValue > 0 ? PlayerMovement.Direction.Right : PlayerMovement.Direction.Left;

            if (newDirection == _currentDirection)
                return;

            _currentDirection = newDirection;
            
            DirectionChanged?.Invoke();
        }
        
        public override void ResetValues()
        {
            _currentDirection = _playerSettings.InitialMoveDirection;
        }
    }
}