﻿using Settings;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Platforms
{
    public class PlatformWithJewel : Platform
    {
        [SerializeField, Required] private PlatformJewelController _platformJewelController;
        
        protected override void OnValidate()
        {
            base.OnValidate();
            
            if (_platformJewelController == null)
                _platformJewelController = GetComponentInChildren<PlatformJewelController>();
        }

        public override bool TryToInit(PlatformsSettings plaformsSettings)
        {
            if(!base.TryToInit(plaformsSettings))
                return false;
            
            if(_platformJewelController)
                _platformJewelController.Initialize(plaformsSettings);

            return true;
        }

        public override void OnSpawn(Transform parent, Vector3 spawnPoint, Quaternion rotation)
        {
            base.OnSpawn(parent, spawnPoint, rotation);
            
            _platformJewelController.TryToSpawnJewel();
        }

        public override void OnDespawnFinished()
        {
            base.OnDespawnFinished();
            
            _platformJewelController.TryToDespawnJewel();
        }
    }
}