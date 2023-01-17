using Cinemachine;
using GameStateMachine.GameStates;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Cameras
{
    public class GameStateCameraController : MonoBehaviour
    {
        [SerializeField, Required] private CinemachineVirtualCamera _defaultCamera;

        private SignalBus _signalBus;

        [Inject]
        private void Init(SignalBus signalBus)
        {
            _signalBus = signalBus;
            
            _signalBus.Subscribe<OnGameStateChanged>(OnGameStateChanged);
        }

        private void OnDestroy()
        {
            _signalBus.Unsubscribe<OnGameStateChanged>(OnGameStateChanged);
        }
        
        private void OnGameStateChanged(OnGameStateChanged stateChangedEvent)
        {
            switch (stateChangedEvent.currentStateType)
            {
                case GameStateType.MainMenu:
                    _defaultCamera.enabled = true;
                    break;
                case GameStateType.SkinShop:
                    _defaultCamera.enabled = false;
                    break;
            }
        }
    }
}
