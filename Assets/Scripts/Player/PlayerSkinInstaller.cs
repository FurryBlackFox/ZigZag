using System;
using System.Collections;
using System.Collections.Generic;
using Installers.GlobalManagers;
using Player;
using Signals;
using UnityEngine;
using Zenject;

public class PlayerSkinInstaller : MonoBehaviour
{
    [SerializeField] private Transform _skinSpawnParent;
    
    private SignalBus _signalBus;
    private PlayerSkinsManager _playerSkinsManager;

    private PlayerSkin _currentPlayerSkin;

    [Inject]
    private void Init(SignalBus signalBus, PlayerSkinsManager playerSkinsManager)
    {
        _signalBus = signalBus;
        _playerSkinsManager = playerSkinsManager;
        
        _signalBus.Subscribe<OnPlayerSkinSelected>(OnPlayerSkinSelected);

        _currentPlayerSkin = GetComponentInChildren<PlayerSkin>();
    }

    private void OnDestroy()
    {
        _signalBus.Unsubscribe<OnPlayerSkinSelected>(OnPlayerSkinSelected);
    }

    private void OnPlayerSkinSelected(OnPlayerSkinSelected skinSelectedEvent)
    {
        TryToChangeSkin(skinSelectedEvent.skinId);
    }

    private void Start()
    {
        TryToChangeSkin(_playerSkinsManager.ActiveSkinId);
    }

    private void TryToChangeSkin(int skinId)
    {
        if(_currentPlayerSkin && _currentPlayerSkin.SkinId == skinId)
            return;
        
        if(_currentPlayerSkin)
            Destroy(_currentPlayerSkin.gameObject);

        var newSkinData = _playerSkinsManager.GetSkinDataFromSkinId(skinId);

        var newSkin = Instantiate(newSkinData.PlayerSkinPrefab, _skinSpawnParent);
        _currentPlayerSkin = newSkin;
    }
}
