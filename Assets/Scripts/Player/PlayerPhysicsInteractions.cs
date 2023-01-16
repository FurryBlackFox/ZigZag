using System;
using Settings;
using Signals;
using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerPhysicsInteractions : MonoBehaviour
    {
        private PlayerSettings _playerSettings;
        private SignalBus _signalBus;
        
        [Inject]
        private void Init(PlayerSettings playerSettings, SignalBus signalBus)
        {
            _playerSettings = playerSettings;
            _signalBus = signalBus;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<DeathZone.DeathZone>(out _))
            {
                _signalBus.Fire<OnPlayerDeath>();
                return;
            }

            if (other.TryGetComponent<Jewel.Jewel>(out var jewel))
            {
                _signalBus.Fire(new OnPlayerCollectedJewel(transform.position, jewel));
                jewel.TryToDespawn();
            }
        }
    }
}