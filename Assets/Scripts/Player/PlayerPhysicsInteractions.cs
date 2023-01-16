using System;
using System.Collections.Generic;
using Platforms;
using PlatformsManager;
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

        private List<PlatformSafeZone> _activeSafeZones = new List<PlatformSafeZone>();

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

            if (other.TryGetComponent<PlatformAiDirection>(out var aiDirection))
            {
                _signalBus.Fire(new OnPlayerCollidedAiDirectionTrigger(aiDirection));
            }

            if (other.TryGetComponent<PlatformSafeZone>(out var platformSafeZone))
            {
                if(!_activeSafeZones.Contains(platformSafeZone))
                    _activeSafeZones.Add(platformSafeZone);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<PlatformSafeZone>(out var platformSafeZone))
            {
                TryToDeleteSafeZoneFromList(platformSafeZone);
            }
        }

        private void TryToDeleteSafeZoneFromList(PlatformSafeZone platformSafeZone)
        {
            if(!_activeSafeZones.Contains(platformSafeZone))
                return;

            _activeSafeZones.Remove(platformSafeZone);

            if (_activeSafeZones.Count == 0)
            {
                _signalBus.Fire<OnPlayerDeath>();
            }
        }
    }
}