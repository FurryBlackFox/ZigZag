using Installers.GlobalManagers;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GlobalManagersInstaller : MonoInstaller
    {
        [SerializeField, Required] private GameStateMachine.GameStateMachine _gameStateMachine;
        
        public override void InstallBindings()
        {
            Container.Bind<PlayerResourcesManager>().FromNew().AsSingle().NonLazy();
            Container.Bind<PlayerSkinsManager>().FromNew().AsSingle().NonLazy();
            Container.Bind<VfxManager>().FromNew().AsSingle().NonLazy();
            
            
            Container.Bind<GameStateMachine.GameStateMachine>().FromInstance(_gameStateMachine).AsSingle().NonLazy();
        }
    }
}