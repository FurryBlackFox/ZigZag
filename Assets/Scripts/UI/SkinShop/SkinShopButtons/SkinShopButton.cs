using Installers.GlobalManagers;
using Settings;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace UI.SkinShopButtons
{
    public class SkinShopButton : MonoBehaviour
    {
        [SerializeField, Required] private SkinShopButtonPurchasedState _purchasedState;
        [SerializeField, Required] private SkinShopButtonNotPurchasedState _notPurchasedState;

        private PlayerSkinData _playerSkinData;
        
        private SignalBus _signalBus;
        
        private PlayerSkinsManager _playerSkinsManager;
        
        public void Initialize(SignalBus signalBus, PlayerSkinData playerSkinData, PlayerSkinsManager playerSkinsManager)
        {
            _signalBus = signalBus;
            _playerSkinData = playerSkinData;
            _playerSkinsManager = playerSkinsManager;
            
            _signalBus.Subscribe<OnPlayerSkinPurchased>(OnPlayerSkinPurchased);
            
            InitializeStates();
        }

        private void OnDestroy()
        {
            _signalBus.Unsubscribe<OnPlayerSkinPurchased>(OnPlayerSkinPurchased);
        }

        private void OnValidate()
        {
            if (_purchasedState == null)
                _purchasedState = GetComponentInChildren<SkinShopButtonPurchasedState>();

            if (_notPurchasedState == null)
                _notPurchasedState = GetComponentInChildren<SkinShopButtonNotPurchasedState>();
        }

        private void InitializeStates()
        {
            _purchasedState.Initialize(_signalBus, _playerSkinData, _playerSkinsManager);
            _notPurchasedState.Initialize(_signalBus, _playerSkinData, _playerSkinsManager);

            var initialPurchasedState = _playerSkinsManager.CheckIfSkinPurchased(_playerSkinData);

            if (initialPurchasedState)
            {
                _purchasedState.Enter();
            }
            else
                _notPurchasedState.Enter();
        }

        private void OnPlayerSkinPurchased(OnPlayerSkinPurchased skinPurchasedEvent)
        {
            if (skinPurchasedEvent.skinId != _playerSkinData.SkinId)
                return;
            
            _notPurchasedState.Exit();
            
            _purchasedState.Enter();
        }
        
    }
}