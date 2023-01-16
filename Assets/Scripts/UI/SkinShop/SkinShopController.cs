using System;
using System.Collections.Generic;
using Installers.GlobalManagers;
using Lean.Pool;
using Settings;
using Sirenix.OdinInspector;
using UI.SkinShopButtons;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.SkinShop
{
    public class SkinShopController : MonoBehaviour
    {
        [SerializeField, Required] private RectTransform _spawnParent;
        
        private List<SkinShopButton> _skinShopButtons = new List<SkinShopButton>();
        
        private PlayerSkinsManager _playerSkinsManager;
        private UISettings _uiSettings;
        private SignalBus _signalBus;
        
        [Inject]
        private void Init(SignalBus signalBus, PlayerSkinsManager playerSkinsManager, UISettings uiSettings)
        {
            _signalBus = signalBus;
            _playerSkinsManager = playerSkinsManager;
            _uiSettings = uiSettings;
            
            SpawnSkinShopButtons();
        }

        private void SpawnSkinShopButtons()
        {
            foreach (var playerSkinData in _playerSkinsManager.GetSkinData())
            {
                var newButton = LeanPool.Spawn(_uiSettings.SkinShopButton, _spawnParent);
                _skinShopButtons.Add(newButton);
                _playerSkinsManager.CheckIfSkinPurchased(playerSkinData);
                newButton.Initialize(_signalBus, playerSkinData, _playerSkinsManager);
            }
        }
    }
}