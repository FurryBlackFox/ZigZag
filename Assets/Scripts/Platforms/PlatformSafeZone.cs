using Sirenix.OdinInspector;
using UnityEngine;

namespace Platforms
{
    public class PlatformSafeZone : MonoBehaviour
    {
        [SerializeField, Required] private Platform _platform;
        [SerializeField, Required] private Collider _safeZoneTrigger;

        private void OnValidate()
        {
            if (_platform == null)
                _platform = GetComponentInParent<Platform>();

            if (_safeZoneTrigger == null)
                _safeZoneTrigger = GetComponent<Collider>();
        }

        private void Awake()
        {
            _platform.OnSpawnEvent += OnPlatformSpawn;
            _platform.OnDespawnStartedEvent += OnPlatformDespawnStarted;
        }

        private void OnDestroy()
        {
            _platform.OnSpawnEvent -= OnPlatformSpawn;
            _platform.OnDespawnStartedEvent -= OnPlatformDespawnStarted;
        }
        
        private void OnPlatformSpawn()
        {
            SetSafeZoneEnabled(true);
        }

        private void OnPlatformDespawnStarted()
        {
            SetSafeZoneEnabled(false);
        }

        private void SetSafeZoneEnabled(bool state)
        {
            _safeZoneTrigger.enabled = state;
        }
    }
}