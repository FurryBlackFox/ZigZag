using System;
using Settings;
using Settings.Audio;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Installers.GlobalManagers
{
    public class AudioPlayer : MonoBehaviour
    {
        [SerializeField, Required] private AudioSource _audioSource;

        private AudioSetupSettings _audioSetupSettings;

        [Inject]
        private void Init(AudioSetupSettings audioSetupSettings)
        {
            _audioSetupSettings = audioSetupSettings;
        }

        public void PlaySound(SfxType sfxType)
        {
            var audioData = _audioSetupSettings.GetAudioDataBySfxType(sfxType);
            _audioSource.PlayOneShot(audioData._audioClip, _audioSetupSettings.Volume * audioData._volume);
        }
        
        private void OnValidate()
        {
            if (_audioSource == null)
                _audioSource = GetComponentInChildren<AudioSource>();
        }
    }
}