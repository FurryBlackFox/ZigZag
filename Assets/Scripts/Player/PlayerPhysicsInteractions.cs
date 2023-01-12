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
        
        [Inject]
        private void Init(PlayerSettings playerSettings)
        {
            _playerSettings = playerSettings;
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
            if (other.CompareTag("DeathZone"))
                OnDeathZoneTriggerEntered?.Invoke();
        }
    }
}