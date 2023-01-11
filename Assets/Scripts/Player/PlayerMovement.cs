using System;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private enum Direction
        {
            None = 0,
            Right = 1,
            Left = 2,
        }
        
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Direction _initialDirection = Direction.Right;
        
        [SerializeField] private float _defaultMovementSpeed = 5f;

        private Direction _currentDirection = Direction.None;

        private void OnValidate()
        {
            if (_rigidbody == null)
                _rigidbody = GetComponentInChildren<Rigidbody>();
        }

        public void StartMovement()
        {
            SetDirection(_initialDirection);
        }
        
        public void ChangeDirection()
        {
            SetDirection(_currentDirection == Direction.Right ? Direction.Left : Direction.Right);
        }

        private void SetDirection(Direction direction)
        {
            _currentDirection = direction;

            var currentVectorDirection = direction switch
            {
                Direction.Right => Vector3.right,
                Direction.Left => Vector3.left,
                _ => Vector3.zero
            };
            currentVectorDirection *= _defaultMovementSpeed;
            _rigidbody.velocity = new Vector3(currentVectorDirection.x, _rigidbody.velocity.y, currentVectorDirection.z);
        }
    }
}
