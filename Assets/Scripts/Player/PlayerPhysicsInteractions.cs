using System;
using Settings;
using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerPhysicsInteractions : MonoBehaviour
    {
        public event Action OnDeathZoneTriggerEntered;

        public bool IsOnGround { get; private set; }
        
        private PlayerSettings _playerSettings;
        private SignalBus _signalBus;
        
        [Inject]
        private void Init(PlayerSettings playerSettings, SignalBus signalBus)
        {
            _playerSettings = playerSettings;
            _signalBus = signalBus;
        }
        
        public void FixedTick()
        {
            // var currentIsOnGroundState = Physics.Raycast(transform.position, Vector3.down,
            //     _playerSettings.GroundDetectionRange, _playerSettings.GroundDetectionLayers);
            //
            // if (!currentIsOnGroundState)
            //     OnDeathZoneTriggerEntered?.Invoke();
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<DeathZone.DeathZone>(out _))
            {
                OnDeathZoneTriggerEntered?.Invoke();
                return;
            }

            if (other.TryGetComponent<Jewel.Jewel>(out var jewel))
            {
                _signalBus.Fire<OnPlayerCollectedJewel>();
                jewel.TryToDespawn();
            }
        }
    }
}