using System;
using System.Collections.Generic;
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

        private void OnValidate()
        {
            if (_jewelSpawnPoint == null)
                _jewelSpawnPoint = GetComponentInChildren<PlatformJewelSpawnPoint>();
        }

        public void Initialize(PlatformsSettings platformsSettings)
        {
            _platformsSettings = platformsSettings;
        }
        
        public void TryToSpawnJewel()
        {
            if (Random.value > _platformsSettings.JewelSpawnProbablility)
                return;

            if (_activeJewel)
                return;

            _activeJewel = LeanPool.Spawn(_platformsSettings.GetRandomJewelPrefab());
            
            var jewelSpawnPointTransform = _jewelSpawnPoint.transform;

            _activeJewel.OnSpawn(jewelSpawnPointTransform,
                jewelSpawnPointTransform.position, jewelSpawnPointTransform.rotation);

            _activeJewel.OnJewelDespawn += OnJewelDespawn;
        }

        public void TryToDespawnJewel()
        {
            if (!_activeJewel)
                return;
            
            _activeJewel.TryToDespawn();
        }
        
        private void OnJewelDespawn()
        {
            _activeJewel.OnJewelDespawn -= OnJewelDespawn;
            _activeJewel = null;
        }
    }
}