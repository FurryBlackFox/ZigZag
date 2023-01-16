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
                SpawnPlatform(null, _platformsSettings.StartPlatformPrefab);
                return true;
            }

            var spawnPoint = _firstPlatform.TryToGetValidSpawnPointAtBounds(_platformsSettings.SpawnBounds);
            if (spawnPoint == null)
                return false;
            
            SpawnPlatform(spawnPoint);
            return true;
        }

        private void SpawnPlatform(PlatformSpawnPoint platformSpawnPoint, Platform concretePlatform = null)
        {
            var targetPlatformPrefab = concretePlatform == null
                ? _platformsSettings.GetRandomPlatformPrefab()
                : _platformsSettings.StartPlatformPrefab;

            var spawnPosition = platformSpawnPoint ? platformSpawnPoint.transform.position : Vector3.zero;
            var spawnRotation = platformSpawnPoint ? platformSpawnPoint.transform.rotation : transform.rotation;
            
            var newPlatform = LeanPool.Spawn(targetPlatformPrefab, spawnPosition, spawnRotation, transform);
            newPlatform.TryToInit(_platformsSettings, _signalBus);
            

            newPlatform.OnSpawn();
            
            if(_firstPlatform)
                _firstPlatform.RegisterNextPlatformAtPoint(newPlatform);
            
            _firstPlatform = newPlatform;
            
            SpawnedPlatforms.Enqueue(_firstPlatform);
            ActivePlatforms.Enqueue(_firstPlatform);


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