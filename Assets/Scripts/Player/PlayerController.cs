using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private PlayerCollision _playerCollision;

        private void OnValidate()
        {
            if (_playerInput == null)
                _playerInput = GetComponentInChildren<PlayerInput>();

            if (_playerMovement == null)
                _playerMovement = GetComponentInChildren<PlayerMovement>();
            
            if (_playerCollision == null)
                _playerCollision = GetComponentInChildren<PlayerCollision>();
        }

        private void Start()
        {
            _playerMovement.StartMovement();
        }

        private void OnEnable()
        {
            _playerInput.DirectionChanged += _playerMovement.ChangeDirection;
            _playerCollision.OnDeathZoneTriggerEntered += OnDeathZoneEntered;
        }

        private void OnDisable()
        {
            _playerInput.DirectionChanged -= _playerMovement.ChangeDirection;
            _playerCollision.OnDeathZoneTriggerEntered -= OnDeathZoneEntered;
        }

        private void Update()
        {
            _playerInput.UpdateTick();
        }

        private void OnDeathZoneEntered()
        {
            Debug.LogError("DeathZone");
            enabled = false;
            Invoke(nameof(ReloadLevel), 2f);
        }

        private void ReloadLevel()
        {
            SceneManager.LoadSceneAsync(0);
        }
    }
}
