using System;
using Platforms;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PlatformsManager
{
    [RequireComponent(typeof(Collider))]
    public class PlatformAiDirection : MonoBehaviour
    {
        [SerializeField, Required] private Platform _platform;
        [SerializeField, Required] private Collider _triggerCollider;
        [SerializeField] private bool _calculateDirection = true;

        private void OnValidate()
        {
            if (_platform == null)
                _platform = GetComponentInParent<Platform>();

            if (_triggerCollider == null)
                _triggerCollider = GetComponent<Collider>();
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawRay(transform.position, transform.forward);
        }

        private void Awake()
        {
            _platform.OnNextPlatformSpawned += OnNextPlatformSpawned;
            _platform.OnDespawnStartedEvent += OnPlatformDespawn;
            
            
            SetEnabledTrigger(false);
        }

        private void OnDestroy()
        {
            _platform.OnNextPlatformSpawned -= OnNextPlatformSpawned;
            _platform.OnDespawnStartedEvent -= OnPlatformDespawn;
        }

        private void OnNextPlatformSpawned(Platform nextPlatform)
        {
            SetEnabledTrigger(true);

            if (!_calculateDirection)
                return;
            
            CalculateForwardDirection(nextPlatform.transform.position);
        }

        private void CalculateForwardDirection(Vector3 nextPlatformPosition)
        {
            var directionToPlatform = (nextPlatformPosition - transform.position).normalized;
            directionToPlatform.y = 0;
            
            transform.forward = directionToPlatform;
        }

        private void OnPlatformDespawn()
        {
            SetEnabledTrigger(false);
        }

        private void SetEnabledTrigger(bool state)
        {
            _triggerCollider.enabled = state;
        }
    }
}