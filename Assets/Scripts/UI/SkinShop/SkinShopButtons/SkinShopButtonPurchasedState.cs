using System;
using Installers.GlobalManagers;
using Settings;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.SkinShopButtons
{
    public class SkinShopButtonPurchasedState : SkinShopButtonAbstractState
    {
        [SerializeField, Required] private Image _previewImage;
        [SerializeField] private Color _activeColor;
        [SerializeField] private Color _inactiveColor;

        public override void Initialize(SignalBus signalBus,
            PlayerSkinData playerSkinData, PlayerSkinsManager playerSkinsManager)
        {
            base.Initialize(signalBus, playerSkinData, playerSkinsManager);

            _previewImage.sprite = base.playerSkinData.Sprite;
        }

        public override void Enter()
        {
            base.Enter();
            signalBus.Subscribe<OnPlayerSkinSelected>(OnPlayerSkinSelected);
            ChangeActiveState(playerSkinData.SkinId == playerSkinsManager.ActiveSkinId);
        }

        protected override void ChangeActiveState(bool state)
        {
            buttonBackground.color = state ? _activeColor : _inactiveColor;
        }

        public override void Exit()
        {
            buttonBackground.color = buttonBgDefaultColor;
            signalBus.Unsubscribe<OnPlayerSkinSelected>(OnPlayerSkinSelected);
            base.Exit();
        }
        
        private void OnPlayerSkinSelected(OnPlayerSkinSelected skinSelectedEvent)
        {
            ChangeActiveState(skinSelectedEvent.skinId == playerSkinData.SkinId);
        }

        protected override void OnButtonClick()
        {
            playerSkinsManager.TryToEnableSkin(playerSkinData.SkinId);
        }
    }
}