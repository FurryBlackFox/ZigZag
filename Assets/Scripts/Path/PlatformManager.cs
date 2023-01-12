using System;
using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using Settings;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Platforms
{
    public class PlatformManager : MonoBehaviour
    {
        [SerializeField] 
        
        private Queue<Platform> _spawnedPlatforms = new Queue<Platform>();
        private Queue<Platform> _activePlatforms = new Queue<Platform>();


        private Platform _firstPlatform;
        private PlatformsSettings _platformsSettings;
        
        [Inject]
        private void Init(PlatformsSettings platformsSettings)
        {
            _platformsSettings = platformsSettings;
        }
        

        private void Start()
        {
            InitialSpawn();
        }

        private void FixedUpdate()
        {
            UpdatePlatformsPositions();
            
            TryToSpawnPlatform();
            TryToDespawnLastPlatform();
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
                SpawnPlatform(Vector3.zero, transform.rotation, _platformsSettings.StartPlatformPrefab);
                return true;
            }

            var spawnPoint = _firstPlatform.TryToGetValidSpawnPointAtBounds(_platformsSettings.SpawnBounds);
            if (spawnPoint == null)
                return false;
            
            SpawnPlatform(spawnPoint.transform.position, spawnPoint.transform.rotation);
            return true;
            
        }

        private void SpawnPlatform(Vector3 spawnPoint, Quaternion rotation, Platform platform = null)
        {
            var targetPlatformPrefab = platform == null
                ? _platformsSettings.GetRandomPlatformPrefab()
                : _platformsSettings.StartPlatformPrefab;
            
            _firstPlatform = LeanPool.Spawn(targetPlatformPrefab);
            _firstPlatform.TryToInit(_platformsSettings);
            
            _spawnedPlatforms.Enqueue(_firstPlatform);
            _activePlatforms.Enqueue(_firstPlatform);
            _firstPlatform.OnSpawn(transform, spawnPoint, rotation);
        }
        
        private void UpdatePlatformsPositions()
        {
            var moveVector = Vector3.back * _platformsSettings.PlatformsSpeed;
            
            foreach (var platform in _spawnedPlatforms)
            {
                platform.Move(moveVector, Time.fixedDeltaTime);
            }
        }
        
        private bool TryToDespawnLastPlatform()
        {
            var lastPlatform = _activePlatforms.Peek();
            if (lastPlatform.CheckIsInGameBounds())
                return false;

            StartCoroutine(DespawnPlatform(lastPlatform));
            return true;
        }

        private IEnumerator DespawnPlatform(Platform platform)
        {
            _activePlatforms.Dequeue();
            platform.OnDespawnStarted();

            yield return new WaitForSeconds(_platformsSettings.PlatformsDeleteDelay);
            
            _spawnedPlatforms.Dequeue();
            LeanPool.Despawn(platform);
        }
    }
}