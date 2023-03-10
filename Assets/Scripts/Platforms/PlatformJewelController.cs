using Lean.Pool;
using Settings;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Platforms
{
    public class PlatformJewelController : MonoBehaviour
    {
        [SerializeField, Required] private PlatformJewelSpawnPoint _jewelSpawnPoint;
        
        private Jewel.Jewel _activeJewel;
        private PlatformsSettings _platformsSettings;

        private SignalBus _signalBus;

        private void OnValidate()
        {
            if (_jewelSpawnPoint == null)
                _jewelSpawnPoint = GetComponentInChildren<PlatformJewelSpawnPoint>();
        }

        public void Initialize(PlatformsSettings platformsSettings, SignalBus signalBus)
        {
            _platformsSettings = platformsSettings;
            _signalBus = signalBus;
        }
        
        public void TryToSpawnJewel()
        {
            if (Random.value > _platformsSettings.JewelSpawnProbablility)
                return;

            if (_activeJewel)
                return;

            var jewelSpawnPointTransform = _jewelSpawnPoint.transform;
            _activeJewel = LeanPool.Spawn(_platformsSettings.GetRandomJewelPrefab(),
                jewelSpawnPointTransform.position, jewelSpawnPointTransform.rotation, jewelSpawnPointTransform);
            _activeJewel.Init(_signalBus);
            _activeJewel.OnJewelDespawned += OnActiveJewelDespawned;

            _activeJewel.OnSpawn();
        }

        public void TryToDespawnJewel()
        {
            if (!_activeJewel)
                return;
            
            _activeJewel.TryToDespawn();
        }

        private void OnActiveJewelDespawned()
        {
            _activeJewel.OnJewelDespawned -= OnActiveJewelDespawned;

            _activeJewel = null;
        }
    }
}