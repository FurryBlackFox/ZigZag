using Settings;
using UnityEngine;
using Zenject;

public class SettingsInstaller : MonoInstaller
{
    [SerializeField] private PlayerSettings _playerSettings;
    [SerializeField] private PlatformsSettings _platformsSettings;

    public override void InstallBindings()
    {
        Container.Bind<PlayerSettings>().FromScriptableObject(_playerSettings).AsSingle();
        Container.Bind<PlatformsSettings>().FromScriptableObject(_platformsSettings).AsSingle();
    }
}