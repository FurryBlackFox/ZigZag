using System;
using Installers.GlobalManagers;
using Settings.Audio;
using UnityEngine;
using Zenject;

public abstract class AbstractUiButtonWithSfx : MonoBehaviour
{
    [SerializeField] protected SfxType sfxType = SfxType.None;
    
    protected abstract event Action PlaySfxEvent;

    protected AudioPlayer audioPlayer;

    [Inject]
    private void Init(AudioPlayer audioPlayer)
    {
        this.audioPlayer = audioPlayer;
        
    }
    
    protected virtual void Awake()
    {
        PlaySfxEvent += OnPlaySfxEvent;
    }
    
    protected virtual void OnDestroy()
    {
        PlaySfxEvent += OnPlaySfxEvent;
    }
    
    private void OnPlaySfxEvent()
    {
        audioPlayer.PlaySound(sfxType);
    }
}
