using System;
using Installers.GlobalManagers;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GlobalManagersInstaller : MonoInstaller
    {
        [SerializeField, Required] private GameStateMachine.GameStateMachine _gameStateMachine;
        [SerializeField, Required] private AudioPlayer _audioPlayer;

        private void OnValidate()
        {
            if (_gameStateMachine == null)
                _gameStateMachine = GetComponentInChildren<GameStateMachine.GameStateMachine>();
            
            if (_audioPlayer == null)
                _audioPlayer = GetComponentInChildren<AudioPlayer>();
        }

        public override void InstallBindings()
        {
            Container.Bind<PlayerResourcesManager>().FromNew().AsSingle().NonLazy();
            Container.Bind<PlayerSkinsManager>().FromNew().AsSingle().NonLazy();
            Container.Bind<VfxManager>().FromNew().AsSingle().NonLazy();
            Container.Bind<SettingsManager>().FromNew().AsSingle().NonLazy();

            Container.Bind<AudioPlayer>().FromInstance(_audioPlayer).AsSingle().NonLazy();

            Container.Bind<GameStateMachine.GameStateMachine>().FromInstance(_gameStateMachine).AsSingle().NonLazy();
        }
    }
}