using System;
using System.Collections.Generic;
using Lean.Pool;
using Settings;
using UnityEngine;
using UnityEngine.Serialization;

namespace Platforms
{
    public class PlatformManager : MonoBehaviour
    {
        [SerializeField] private PlaformsSettings _plaformsSettings;
        
        private Queue<Platform> _activePlatforms = new Queue<Platform>();

        private Platform _firstPlatform;


        private void Start()
        {
            InitialSpawn();
        }

        private void FixedUpdate()
        {
            UpdatePlatformsPositions();
            
            TryToSpawnPlatform();
            TryToDeleteLastPlatform();
        }


        private void InitialSpawn()
        {
            var spawnResult = false;
            do
            {
                spawnResult = TryToSpawnPlatform();
            } while (spawnResult);
        }

        private bool TryToSpawnPlatform()
        {
            if (_firstPlatform == null)
            {
                SpawnPlatform(Vector3.zero, transform.rotation, _plaformsSettings.StartPlatformPrefab);
                return true;
            }

            var spawnPoint = _firstPlatform.TryToGetValidSpawnPointAtBounds(_plaformsSettings.SpawnBounds);
            if (spawnPoint == null)
                return false;
            
            SpawnPlatform(spawnPoint.transform.position, spawnPoint.transform.rotation);
            return true;
            
        }

        private void SpawnPlatform(Vector3 spawnPoint, Quaternion rotation, Platform platform = null)
        {
            var targetPlatformPrefab = platform == null
                ? _plaformsSettings.GetRandomPlatformPrefab()
                : _plaformsSettings.StartPlatformPrefab;
            
            _firstPlatform = LeanPool.Spawn(targetPlatformPrefab);
            
            
            _activePlatforms.Enqueue(_firstPlatform);
            _firstPlatform.OnSpawn(transform, spawnPoint, rotation);
        }
        
        private void UpdatePlatformsPositions()
        {
            var moveVectorDelta = Vector3.back * _plaformsSettings.PlatformsSpeed * Time.fixedDeltaTime;
            foreach (var platform in _activePlatforms)
            {
                platform.Move(moveVectorDelta);
            }
        }
        
        private bool TryToDeleteLastPlatform()
        {
            var lastPlatform = _activePlatforms.Peek();
            if (lastPlatform.IsInGameBounds(_plaformsSettings.DestroyDistanceFromCenter))
                return false;
            
            lastPlatform.OnDespawn();
            LeanPool.Despawn(lastPlatform);
            _activePlatforms.Dequeue();
            return true;
        }
    }
}