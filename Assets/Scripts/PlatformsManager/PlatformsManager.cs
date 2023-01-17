using GameStateMachine.GameStates;
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
            var prevStateType = stateChangedEvent.prevStateType;
            switch (stateChangedEvent.currentStateType)
            {
                case GameStateType.MainMenu when prevStateType == GameStateType.None ||
                                                 prevStateType == GameStateType.Defeat || 
                                                 prevStateType == GameStateType.Pause:
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