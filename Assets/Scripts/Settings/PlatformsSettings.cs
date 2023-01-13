using System.Collections.Generic;
using Lean.Pool;
using Platforms;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "PlaformsSettings", menuName = "App/PlaformsSettings", order = 0)]
    public class PlatformsSettings : ScriptableObject
    {
        [field: TitleGroup("Platforms Spawn")]
        [field: SerializeField] public Vector2 SpawnBounds { get; private set; } = new Vector2(10f, 20f);
        [field: SerializeField, Min(0f)] public float MaxPlatformDistanceFromCenterToDestroy { get; private set; } = 10f;
        [field: SerializeField] public Platform StartPlatformPrefab { get; private set; }
        [field: SerializeField] public List<Platform> PlatformTypePrefabs { get; private set; }

        [field: TitleGroup("Platforms Movement")]
        [field: SerializeField] public float PlatformsSpeed { get; private set; } = 5f;
        
        [field: TitleGroup("Platforms Despawn")]
        [field: SerializeField, Min(0f)] public float PlatformsFallSpeed { get; private set; } = 5f;
        [field: SerializeField, Min(0f)] public float PlatformsDespawnDelay { get; private set; } = 5f;
        
        [field: TitleGroup("Jewels Spawn")]
        [field: SerializeField, Range(0f, 1f)] public float JewelSpawnProbablility { get; private set; } = 0.2f;
        [field: SerializeField] public List<Jewel.Jewel> JewelPrefabs { get; private set; }

        public Platform GetRandomPlatformPrefab()
        {
            return GetRandomElementFromList(PlatformTypePrefabs);
        }
        
        public Jewel.Jewel GetRandomJewelPrefab()
        {
            return GetRandomElementFromList(JewelPrefabs);
        }

        private T GetRandomElementFromList<T>(List<T> list)
        {
            var randomIndex = Random.Range(0, list.Count);
            return list[randomIndex];
        }
    }
}