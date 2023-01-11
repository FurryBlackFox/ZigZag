using System.Collections.Generic;
using UnityEngine;

namespace Platforms
{
    public class Platform : MonoBehaviour
    {
        [SerializeField] private List<PlatformSpawnPoint> _platformSpawnPoints;

        public void OnSpawn(Transform parent, Vector3 spawnPoint, Quaternion rotation)
        {
            transform.parent = parent;
            transform.position = spawnPoint;
            transform.rotation = rotation;
        }

        public void OnDespawn()
        {
            
        }

        public void Move(Vector3 delta)
        {
            transform.position += delta;
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

        public bool IsInGameBounds(float maxDistanceFromCenter)
        {
            return transform.position.z >= -maxDistanceFromCenter;
        }
    }
}