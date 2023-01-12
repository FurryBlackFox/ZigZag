using System;
using Settings;
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
        
        [SerializeField] private Rigidbody _rigidbody;

        private Direction _currentDirection = Direction.None;
        private Vector3 _currentVectorDirection = Vector3.zero;
        
        private PlayerSettings _playerSettings;
        
        [Inject]
        private void Init(PlayerSettings playerSettings)
        {
            _playerSettings = playerSettings;
        }
        
        private void OnValidate()
        {
            if (_rigidbody == null)
                _rigidbody = GetComponentInChildren<Rigidbody>();
        }

        public void StartMovement()
        {
            SetDirection(_playerSettings.InitialMoveDirection);
        }
        
        public void ChangeDirection()
        {
            SetDirection(_currentDirection == Direction.Right ? Direction.Left : Direction.Right);
        }
        
        private void FixedUpdate()
        {
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
