using Signals;
using Zenject;

namespace Installers
{
    public class SignalsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            
            Container.DeclareSignal<OnGameStateChanged>();
            Container.DeclareSignal<OnGameStateChangeButtonClick>();

            Container.DeclareSignal<OnPlayerCollectedJewel>();
            Container.DeclareSignal<OnPlayerDeath>();
            Container.DeclareSignal<OnPlayerChangedMoveDirection>();
            Container.DeclareSignal<OnPlayerCollidedAiDirectionTrigger>();

            
            Container.DeclareSignal<OnPlayerSkinPurchased>();
            Container.DeclareSignal<OnPlayerSkinSelected>();
            
            Container.DeclareSignal<OnMusicEnabledStateChanged>();
            Container.DeclareSignal<OnPlayerAiInputEnabledStateChanged>();
        }
    }
}