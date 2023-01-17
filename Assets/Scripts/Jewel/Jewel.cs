using System;
using Lean.Pool;
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

        public void OnSpawn()
        {
        }

        public void TryToDespawn()
        {
            OnJewelDespawned?.Invoke();
            LeanPool.Despawn(gameObject);
        }
    }
}