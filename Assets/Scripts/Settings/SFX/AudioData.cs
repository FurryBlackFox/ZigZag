using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Settings.Audio
{
    [Serializable]
    public enum SfxType
    {
        None = 0,
        ButtonClick = 1,
        JewelCollect = 2,
        PlayerDeath = 3,
        PlayerDirectionChanged = 4,
    }
    
    [Serializable]
    public class AudioData
    {
        [field: SerializeField] public SfxType _sfxType = SfxType.None;
        [field:SerializeField, Required] public AudioClip _audioClip;
        [field: SerializeField, Range(0, 1)] public float _volume = 1;
    }
}