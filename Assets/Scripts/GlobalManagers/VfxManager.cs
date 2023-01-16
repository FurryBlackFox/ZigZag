using Lean.Pool;
using Settings;
using Signals;
using UnityEngine;
using Zenject;

namespace Installers.GlobalManagers
{
    public class VfxManager
    {
        private SignalBus _signalBus;
        private VfxSettings _vfxSettings;

        [Inject]
        public void Init(SignalBus signalBus, VfxSettings vfxSettings)
        {
            _signalBus = signalBus;
            _vfxSettings = vfxSettings;
            
            _signalBus.Subscribe<OnPlayerCollectedJewel>(OnPlayerCollectedJewel);
        }

        ~VfxManager()
        {
            _signalBus.Unsubscribe<OnPlayerCollectedJewel>(OnPlayerCollectedJewel);
        }

        private void OnPlayerCollectedJewel(OnPlayerCollectedJewel onPlayerCollectedJewel)
        {
            var jewelPosition = onPlayerCollectedJewel.jewel.transform.position;
            if(_vfxSettings.EnableOnJewelCollectedAnnouncers)
                SpawnOnJewelCollectedAnnouncer(jewelPosition);
        }

        private void SpawnOnJewelCollectedAnnouncer(Vector3 position)
        {
            var announcer = LeanPool.Spawn(_vfxSettings.JewelCollectedAnnouncerPrefab);

            announcer.transform.position = position;
            
            announcer.Play(_vfxSettings.JewelCollectedAnnouncerShowDuration,
                _vfxSettings.JewelCollectedAnnouncerMoveDistance, _vfxSettings.JewelCollectedAnnouncerEase);
            
        }
    }
}