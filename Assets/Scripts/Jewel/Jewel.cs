using System;
using Lean.Pool;
using UnityEngine;

namespace Jewel
{
    public class Jewel : MonoBehaviour
    {
        public event Action OnJewelDespawn; 

        public void OnSpawn(Transform parent, Vector3 spawnPoint, Quaternion rotation)
        {
            transform.parent = parent;
            transform.position = spawnPoint;
            transform.rotation = rotation;
        }

        public void TryToDespawn()
        {
            OnJewelDespawn?.Invoke();
            
            LeanPool.Despawn(gameObject);
        }
    }
}