using System.Collections.Generic;
using Settings;
using UnityEngine;

namespace Platforms
{
    public class Platform : MonoBehaviour
    {
        [SerializeField] private List<PlatformSpawnPoint> _platformSpawnPoints;

        private PlatformsSettings _platformsSettings;

        private bool _initialized = false;

        private bool _isDespawnStarted = false;
        
        public void TryToInit(PlatformsSettings plaformsSettings)
        {
            if(_initialized)
                return;

            _platformsSettings = plaformsSettings;
            
            _initialized = true;
        }
        
        public void OnSpawn(Transform parent, Vector3 spawnPoint, Quaternion rotation)
        {
            _isDespawnStarted = false;
            
            transform.parent = parent;
            transform.position = spawnPoint;
            transform.rotation = rotation;
        }

        public void OnDespawnStarted()
        {
            _isDespawnStarted = true;
        }

        public void Move(Vector3 moveVector, float deltaTime)
        {
            if (_isDespawnStarted)
                moveVector.y -= _platformsSettings.PlatformsFallSpeed;
            
            transform.position += moveVector * deltaTime;
        }
        
        public PlatformSpawnPoint TryToGetValidSpawnPointAtBounds(Vector2 bounds)
        {
            var validPlatformSpawnPoints = new List<PlatformSpawnPoint>();
            
            foreach (var platformSpawnPoint in _platformSpawnPoints)
            {
                if(platformSpawnPoint.IsInBounds(bounds))
                    validPlatformSpawnPoints.Add(platformSpawnPoint);
            }

            if (validPlatformSpawnPoints.Count == 0)
                return null;
            
            var randomIndex = Random.Range(0, validPlatformSpawnPoints.Count);
            return validPlatformSpawnPoints[randomIndex];
        }

        public bool CheckIsInGameBounds()
        {
            return transform.position.z >= -_platformsSettings.MaxPlatformDistanceFromCenterToDestroy;
        }
    }
}