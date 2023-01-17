using Installers.GlobalManagers;
using Settings;
using TMPro;
using UnityEngine;
using Zenject;

namespace UI.SkinShopButtons
{
    public class SkinShopButtonNotPurchasedState : SkinShopButtonAbstractState
    {
        [SerializeField] private TextMeshProUGUI _priceText;
        [SerializeField] private Color _notPurchasedColor;

        public override void Initialize(SignalBus signalBus, 
            PlayerSkinData playerSkinData, PlayerSkinsManager playerSkinsManager)
        {
            base.Initialize(signalBus, playerSkinData, playerSkinsManager);
            _priceText.SetText(playerSkinData.PurchasePrice.ToString());
        }

        public override void Enter()
        {
            base.Enter();
            buttonBackground.color = _notPurchasedColor;
        }

        public override void Exit()
        {
            buttonBackground.color = buttonBgDefaultColor;
            base.Exit();
        }

        protected override void ChangeActiveState(bool state)
        {
            button.interactable = state;
        }

        protected override void OnButtonClick()
        {
            playerSkinsManager.TryToPurchaseSkin(playerSkinData.SkinId);
        }
    }
}