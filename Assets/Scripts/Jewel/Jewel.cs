using System;
using Lean.Pool;
using Signals;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace Jewel
{
    public class Jewel : MonoBehaviour
    {
        public Action OnJewelDespawned;
        private SignalBus _signalBus;

        public void Init(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void OnSpawn(Transform parent, Vector3 spawnPoint, Quaternion rotation)
        {
            transform.parent = parent;
            transform.position = spawnPoint;
            transform.rotation = rotation;
        }

        public void TryToDespawn()
        {
            OnJewelDespawned?.Invoke();
            LeanPool.Despawn(gameObject);
        }
    }
}