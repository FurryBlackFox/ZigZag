using System.Collections.Generic;
using System.Linq;
using Settings.Audio;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "AudioSetupSettings", menuName = "App/AudioSetupSettings", order = 0)]
    public class AudioSetupSettings : ScriptableObject
    {
        private bool MultipleSameSfxTypeErrorStatement =>
            AudioData.Select(data=> data._sfxType).Distinct().Count() < AudioData.Count;
        [field: SerializeField, Range(0, 1)] public float Volume { get; private set; } = 1f;
        
        [field: InfoBox("You can not have multiple SFX of the same type",
            InfoMessageType.Error, nameof(MultipleSameSfxTypeErrorStatement))]
        [field: SerializeField, Required] public List<AudioData> AudioData { get; private set; }

        private Dictionary<SfxType, AudioData> _sfxAudioDataDictionary = new Dictionary<SfxType, AudioData>();

        public AudioData GetAudioDataBySfxType(SfxType sfxType)
        {
            if(_sfxAudioDataDictionary.Count < AudioData.Count)
                _sfxAudioDataDictionary = AudioData.ToDictionary(data => data._sfxType, data => data);

            return _sfxAudioDataDictionary[sfxType];
        }
        
    }
}