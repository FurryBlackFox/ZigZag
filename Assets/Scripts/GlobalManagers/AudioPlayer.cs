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
        private SettingsManager _settingsManager;

        private bool _musicEnabled;
        
        [Inject]
        private void Init(AudioSetupSettings audioSetupSettings, SettingsManager settingsManager)
        {
            _audioSetupSettings = audioSetupSettings;
            _settingsManager = settingsManager;

            UpdateMusicEnabledState();
            _settingsManager.MusicEnabledSavableData.OnValueChanged += OnMusicEnabledStateChanged;
        }

        ~AudioPlayer()
        {
            _settingsManager.MusicEnabledSavableData.OnValueChanged -= OnMusicEnabledStateChanged;
        }

        private void OnMusicEnabledStateChanged()
        {
            UpdateMusicEnabledState();
        }

        private void UpdateMusicEnabledState()
        {
            _musicEnabled = _settingsManager.MusicEnabledSavableData.Value;
        }

        public void PlaySound(SfxType sfxType)
        {
            if(!_musicEnabled)
                return;
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