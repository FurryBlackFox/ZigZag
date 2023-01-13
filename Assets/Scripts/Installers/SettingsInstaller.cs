using Settings;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class SettingsInstaller : MonoInstaller
    {
        [SerializeField, Required] private PlayerSettings _playerSettings;
        [SerializeField, Required] private PlatformsSettings _platformsSettings;
        [SerializeField, Required] private VfxSettings _vfxSettings;
        
        public override void InstallBindings()
        {
            Container.Bind<PlayerSettings>().FromScriptableObject(_playerSettings).AsSingle();
            Container.Bind<PlatformsSettings>().FromScriptableObject(_platformsSettings).AsSingle();
            Container.Bind<VfxSettings>().FromScriptableObject(_vfxSettings).AsSingle();
        }
    }
}