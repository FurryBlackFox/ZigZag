using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using Platforms;
using Settings;
using UnityEngine;
using Zenject;

namespace PlatformsManager
{
    public class PlatformsSpawner : MonoBehaviour
    {
        public Queue<Platform> SpawnedPlatforms { get; private set; } = new Queue<Platform>();
        public Queue<Platform> ActivePlatforms { get; private set; } = new Queue<Platform>();
        
        private Platform _firstPlatform;

        private PlatformsSettings _platformsSettings;
        private SignalBus _signalBus;

        [Inject]
        private void Init(PlatformsSettings platformsSettings, SignalBus signalBus)
        {
            _platformsSettings = platformsSettings;
            _signalBus = signalBus;
        }
        
        public void FixedTick()
        {
            TryToSpawnPlatform();
            TryToDespawnLastPlatform();
        }

        private void TryToForceDespawnAllPlatforms()
        {
            StopAllCoroutines();

            while (SpawnedPlatforms.Count > 0)
            {
                ForceDespawnLastPlatform();
            }
        }

        public void InitialSpawn()
        {
            TryToForceDespawnAllPlatforms();
            
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
            _firstPlatform.TryToInit(_platformsSettings, _signalBus);
            
            SpawnedPlatforms.Enqueue(_firstPlatform);
            ActivePlatforms.Enqueue(_firstPlatform);
            _firstPlatform.OnSpawn(transform, spawnPoint, rotation);
        }
        
        private void ForceDespawnLastPlatform()
        {
            var targetPlatform = SpawnedPlatforms.Peek();
            StartCoroutine(DespawnPlatform(targetPlatform, true));
        }
        
        private bool TryToDespawnLastPlatform()
        {
            if (ActivePlatforms.Count == 0)
                return false;
        
            var targetPlatform = ActivePlatforms.Peek();
            if (targetPlatform.CheckIsInGameBounds())
                return false; 
            

            StartCoroutine(DespawnPlatform(targetPlatform, false));
            return true;
        }

        private IEnumerator DespawnPlatform(Platform platform, bool instant)
        {
            if(ActivePlatforms.Peek() == platform)
                ActivePlatforms.Dequeue();
            
            platform.OnDespawnStarted();

            if(!instant)
                yield return new WaitForSeconds(_platformsSettings.PlatformsDespawnDelay);

            SpawnedPlatforms.Dequeue();
            
            if (_firstPlatform == platform)
                _firstPlatform = null;
            
            platform.OnDespawnFinished();
            LeanPool.Despawn(platform);
        }
    }
}