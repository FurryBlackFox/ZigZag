using System;
using System.Collections.Generic;
using System.Linq;
using Settings;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Platforms
{
    public class Platform : MonoBehaviour
    {
        [SerializeField, Required] private List<PlatformSpawnPoint> _platformSpawnPoints;

        protected PlatformsSettings platformsSettings;
        protected SignalBus signalBus;

        protected bool initialized = false;
        protected bool isDespawnStarted = false;



        protected virtual void OnValidate()
        {
            if (_platformSpawnPoints == null || _platformSpawnPoints.Count == 0)
                _platformSpawnPoints = GetComponentsInChildren<PlatformSpawnPoint>().ToList();
        }

        public virtual bool TryToInit(PlatformsSettings plaformsSettings, SignalBus signalBus)
        {
            if(initialized)
                return false;

            platformsSettings = plaformsSettings;
            this.signalBus = signalBus;

            initialized = true;

            return true;
        }
        
        public virtual void OnSpawn(Transform parent, Vector3 spawnPoint, Quaternion rotation)
        {
            isDespawnStarted = false;
            
            transform.parent = parent;
            transform.position = spawnPoint;
            transform.rotation = rotation;
            
        }

        public virtual void OnDespawnStarted()
        {
            isDespawnStarted = true;
        }

        public virtual void OnDespawnFinished()
        {
            
        }

        public void Move(Vector3 moveVector, float deltaTime)
        {
            if (isDespawnStarted)
                moveVector.y -= platformsSettings.PlatformsFallSpeed;
            
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
            return transform.position.z >= -platformsSettings.MaxPlatformDistanceFromCenterToDestroy;
        }
    }
}