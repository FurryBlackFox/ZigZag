using System.Collections.Generic;
using Lean.Pool;
using Platforms;
using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "PlaformsSettings", menuName = "App/PlaformsSettings", order = 0)]
    public class PlaformsSettings : ScriptableObject
    {
        [field: SerializeField] public Vector2 SpawnBounds { get; private set; } = new Vector2(10f, 20f);
        [field: SerializeField] public float DestroyDistanceFromCenter { get; private set; } = 10f;
        [field: SerializeField] public Platform StartPlatformPrefab { get; private set; }
        [field: SerializeField] public List<Platform> PlatformTypePrefabs { get; private set; }

        [field: SerializeField] public float PlatformsSpeed { get; private set; } = 5f;
      

        public Platform GetRandomPlatformPrefab()
        {
            var randomIndex = Random.Range(0, PlatformTypePrefabs.Count);
            return PlatformTypePrefabs[randomIndex];
        }
    }
}