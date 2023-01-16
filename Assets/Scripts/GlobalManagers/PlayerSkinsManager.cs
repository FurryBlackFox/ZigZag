using System.Collections.Generic;
using DefaultNamespace;
using Settings;
using Signals;
using Utils.SavableData;
using Zenject;

namespace Installers.GlobalManagers
{
    public class PlayerSkinsManager
    {
        private Dictionary<PlayerSkinData, BooleanSavableData> _playerSkinsStateDictionary =
            new Dictionary<PlayerSkinData, BooleanSavableData>();

        private Dictionary<int, PlayerSkinData> _playerSkinIdDataDictionary = new Dictionary<int, PlayerSkinData>();

        private IntegerSavableData _activeSkinId = new IntegerSavableData(StaticData.PlayerActiveSkinKey);
        public int ActiveSkinId => _activeSkinId.Value;
        
        private SignalBus _signalBus;
        private PlayerSkinsContainer _playerSkinsContainer;
        private PlayerResourcesManager _playerResourcesManager;

        [Inject]
        public void Init(SignalBus signalBus, PlayerSkinsContainer playerSkinsContainer,
            PlayerResourcesManager playerResourcesManager)
        {
            _signalBus = signalBus;
            _playerSkinsContainer = playerSkinsContainer;
            _playerResourcesManager = playerResourcesManager;
        
            InitPlayerSkinsDictionary();
            
            LoadData();
        }

        private void InitPlayerSkinsDictionary()
        {
            foreach (var playerSkinData in _playerSkinsContainer.SkinData)
            {
                var savableSkinState = new BooleanSavableData(StaticData.PlayerSkinSaveKey + playerSkinData.SkinId);
                _playerSkinsStateDictionary.Add(playerSkinData, savableSkinState);
                _playerSkinIdDataDictionary.Add(playerSkinData.SkinId, playerSkinData);
            }
        }

        private void LoadData()
        {
            _activeSkinId.Load();
            
            
            foreach (var skinStateData in _playerSkinsStateDictionary)
            {
                var skinOpenStateSavableData = skinStateData.Value;
                skinOpenStateSavableData.Load();
                if(skinOpenStateSavableData.Value == false && skinStateData.Key.IsPurchasedByDefault)
                    skinOpenStateSavableData.SetValue(true);
            }
        }

        public IEnumerable<PlayerSkinData> GetSkinData()
        {
            return _playerSkinsStateDictionary.Keys;
        }
        
        public bool CheckIfSkinPurchased(PlayerSkinData skinData)
        {
            return _playerSkinsStateDictionary[skinData].Value;
        }

        public void TryToPurchaseSkin(int skinId)
        {
            var targetSkinData = GetSkinDataFromSkinId(skinId);
            var writeOffResult = _playerResourcesManager.TryToWriteOffJewels(targetSkinData.PurchasePrice);
            
            if(!writeOffResult)
                return;
            
            var savableSkinData = _playerSkinsStateDictionary[GetSkinDataFromSkinId(skinId)];
            savableSkinData.SetValue(true);
            
            _signalBus.Fire(new OnPlayerSkinPurchased(skinId));
            
            ChangeActiveSkin(skinId);
        }

        public void TryToEnableSkin(int skinId)
        {
            ChangeActiveSkin(skinId);
        }

        private void ChangeActiveSkin(int skinId)
        {
            if(_activeSkinId.Value == skinId)
                return;
            
            _activeSkinId.SetValue(skinId);
            
            _signalBus.Fire(new OnPlayerSkinSelected(ActiveSkinId));
        }

        public PlayerSkinData GetSkinDataFromSkinId(int skinId)
        {
            return _playerSkinIdDataDictionary[skinId];
        }
    }
}