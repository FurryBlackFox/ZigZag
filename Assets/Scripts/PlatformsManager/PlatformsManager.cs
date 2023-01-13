using System;
using GameStateMachine.GameStates;
using Settings;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace PlatformsManager
{
    public class PlatformsManager : MonoBehaviour
    {
        [SerializeField, Required] private PlatformsSpawner _platformsSpawner;
        [SerializeField, Required] private PlatformsMover _platformsMover;
        
        private SignalBus _signalBus;

        [Inject]
        private void Init(SignalBus signalBus)
        {
            _signalBus = signalBus;

            _signalBus.Subscribe<OnGameStateChanged>(OnGameStateChanged);
        }
        
        private void OnDestroy()
        {
            _signalBus.Unsubscribe<OnGameStateChanged>(OnGameStateChanged);
        }
        
        private void OnValidate()
        {
            if (_platformsMover == null)
                _platformsMover = GetComponentInChildren<PlatformsMover>();

            if (_platformsSpawner == null)
                _platformsSpawner = GetComponentInChildren<PlatformsSpawner>();
        }

        
        private void OnGameStateChanged(OnGameStateChanged stateChangedEvent)
        {
            switch (stateChangedEvent.gameStateType)
            {
                case GameStateType.MainMenu:
                    _platformsSpawner.InitialSpawn();
                    _platformsMover.ChangeMovementAvailabilityState(false);
                    break;
                case GameStateType.Play:
                    _platformsMover.ChangeMovementAvailabilityState(true);
                    break;
                case GameStateType.Pause:
                    _platformsMover.ChangeMovementAvailabilityState(false);
                    break;
            }
        }

        private void FixedUpdate()
        {
            _platformsMover.TryToMovePlatforms(_platformsSpawner.SpawnedPlatforms);
            
            _platformsSpawner.FixedTick();
        }

    }
}