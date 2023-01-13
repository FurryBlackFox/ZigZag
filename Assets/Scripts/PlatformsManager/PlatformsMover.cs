using System.Collections.Generic;
using Platforms;
using Settings;
using UnityEngine;
using Zenject;

namespace PlatformsManager
{
    public class PlatformsMover : MonoBehaviour
    {
        private bool _isAbleToMovePlatforms;
        private PlatformsSettings _platformsSettings;

        [Inject]
        private void Init(PlatformsSettings platformsSettings)
        {
            _platformsSettings = platformsSettings;
        }
        
        public void ChangeMovementAvailabilityState(bool newState)
        {
            _isAbleToMovePlatforms = newState;
        }
        
        public void TryToMovePlatforms(IEnumerable<Platform> platforms)
        {
            if(!_isAbleToMovePlatforms)
                return;
            
            UpdatePlatformsPositions(platforms);
        }
        
        private void UpdatePlatformsPositions(IEnumerable<Platform> platforms)
        {
            var moveVector = Vector3.back * _platformsSettings.PlatformsSpeed;
            
            foreach (var platform in platforms)
            {
                platform.Move(moveVector, Time.fixedDeltaTime);
            }
        }
    }
}