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
        [SerializeField] private PlayerPhysicsInteractions _playerPhysicsInteractions;


        private SignalBus _signalBus;
        
        [Inject]
        private void Init(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }
        
        private void OnValidate()
        {
            if (_playerInput == null)
                _playerInput = GetComponentInChildren<PlayerInput>();

            if (_playerMovement == null)
                _playerMovement = GetComponentInChildren<PlayerMovement>();
            
            if (_playerPhysicsInteractions == null)
                _playerPhysicsInteractions = GetComponentInChildren<PlayerPhysicsInteractions>();
        }

        private void Start()
        {
            _playerInput.Enable();
            _playerMovement.StartMovement();
        }

        private void OnEnable()
        {
            _playerInput.DirectionChanged += _playerMovement.ChangeDirection;
            
            _playerPhysicsInteractions.OnDeathZoneTriggerEntered += OnDeathZoneEntered;
        }

        private void OnDisable()
        {
            _playerInput.DirectionChanged -= _playerMovement.ChangeDirection;
            _playerPhysicsInteractions.OnDeathZoneTriggerEntered -= OnDeathZoneEntered;
        }

        private void Update()
        {
            _playerInput.UpdateTick();
        }

        private void FixedUpdate()
        {
            _playerPhysicsInteractions.FixedTick();
        }

        private void OnDeathZoneEntered()
        {
            _playerInput.Stop();
            
            _signalBus.Fire<OnPlayerDeath>();
            
            Invoke(nameof(ReloadLevel), 2f);
        }

        private void ReloadLevel()
        {
            SceneManager.LoadScene(0);
        }
    }
}
