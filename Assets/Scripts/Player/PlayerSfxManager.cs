using System;
using GameStateMachine.GameStates;
using Installers.GlobalManagers;
using Settings.Audio;
using Signals;
using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerSfxManager : MonoBehaviour
    {
        private SignalBus _signalBus;
        private AudioPlayer _audioPlayer;

        private bool _enabled;

        [Inject]
        private void Init(SignalBus signalBus, AudioPlayer audioPlayer)
        {
            _signalBus = signalBus;
            _audioPlayer = audioPlayer;
            
            _signalBus.Subscribe<OnGameStateChanged>(OnGameStateChanged);
            _signalBus.Subscribe<OnPlayerChangedMoveDirection>(OnPlayerChangedMoveDirection);
            _signalBus.Subscribe<OnPlayerDeath>(OnPlayerDeath);
            _signalBus.Subscribe<OnPlayerCollectedJewel>(OnPlayerCollectedJewel);
        }

        private void OnDestroy()
        {
            _signalBus.Unsubscribe<OnGameStateChanged>(OnGameStateChanged);
            _signalBus.Unsubscribe<OnPlayerChangedMoveDirection>(OnPlayerChangedMoveDirection);
            _signalBus.Unsubscribe<OnPlayerDeath>(OnPlayerDeath);
            _signalBus.Unsubscribe<OnPlayerCollectedJewel>(OnPlayerCollectedJewel);
        }

        private void OnPlayerChangedMoveDirection()
        {
            if(!_enabled)
                return;
            
            _audioPlayer.PlaySound(SfxType.PlayerDirectionChanged);
        }

        private void OnPlayerDeath()
        {
            if(!_enabled)
                return;
            
            _audioPlayer.PlaySound(SfxType.PlayerDeath);
        }

        private void OnPlayerCollectedJewel()
        {
            if(!_enabled)
                return;
            
            _audioPlayer.PlaySound(SfxType.JewelCollect);
        }

        private void OnGameStateChanged(OnGameStateChanged stateChangedEvent)
        {
            _enabled = stateChangedEvent.currentStateType == GameStateType.Play;
        }
    }
}
