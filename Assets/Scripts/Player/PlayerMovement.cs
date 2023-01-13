using System;
using Settings;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        public enum Direction
        {
            None = 0,
            Right = 1,
            Left = 2,
        }
        
        [SerializeField, Required] private Rigidbody _rigidbody;

        private Direction _currentDirection = Direction.None;
        private Vector3 _currentVectorDirection = Vector3.zero;
        
        private PlayerSettings _playerSettings;
        private SignalBus _signalBus;

        private bool _isAbleToMove = false;
        
        private Vector3 _initialPosition = Vector3.zero;
        [Inject]
        private void Init(PlayerSettings playerSettings, SignalBus signalBus)
        {
            _playerSettings = playerSettings;
            _signalBus = signalBus;

            _initialPosition = transform.position;
        }

        private void OnValidate()
        {
            if (_rigidbody == null)
                _rigidbody = GetComponentInChildren<Rigidbody>();
        }

        public void ResetValues()
        {
            _rigidbody.MovePosition(_initialPosition);
            SetDirection(_playerSettings.InitialMoveDirection);
        }

        public void ChangeMoveAvailabilityState(bool newState)
        {
            _isAbleToMove = newState;
            
            if(_isAbleToMove)
                return;
            
            _rigidbody.velocity = new Vector3(0f, _rigidbody.velocity.y, 0f);
        }

        public void ChangeDirection()
        {
            _signalBus.Fire<OnPlayerChangedMoveDirection>();
            SetDirection(_currentDirection == Direction.Right ? Direction.Left : Direction.Right);
        }
        
        private void FixedUpdate()
        {
            TryToMovePlayer();
        }

        private void TryToMovePlayer()
        {
            if(!_isAbleToMove)
                return;
            
            _rigidbody.velocity = new Vector3(_currentVectorDirection.x, _rigidbody.velocity.y, _currentVectorDirection.z);
        }

        private void SetDirection(Direction direction)
        {
            _currentDirection = direction;

            _currentVectorDirection = direction switch
            {
                Direction.Right => Vector3.right,
                Direction.Left => Vector3.left,
                _ => Vector3.zero
            };
            _currentVectorDirection *= _playerSettings.DefaultMovementSpeed;
        }
    }
}
