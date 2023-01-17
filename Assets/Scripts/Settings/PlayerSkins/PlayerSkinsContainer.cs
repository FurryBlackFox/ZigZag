using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "PlayerSkinsContainer", menuName = "App/PlayerSkinsContainer", order = 0)]
    public class PlayerSkinsContainer : ScriptableObject
    {
        private bool ZeroDefaultSkinsErrorStatement => GetDefaultSkinsCount() == 0;
        private bool MultipleDefaultSkinsErrorStatement => GetDefaultSkinsCount() > 1;
        
        [field: InfoBox("You need to have at least one default skin", 
            InfoMessageType.Error, nameof(ZeroDefaultSkinsErrorStatement))]
        [field: InfoBox("You can not have more then one default skin", 
            InfoMessageType.Error, nameof(MultipleDefaultSkinsErrorStatement))]
        [field: SerializeField] public List<PlayerSkinData> SkinData { get; set; }

        public PlayerSkinData GetSkinDataBySkinId(int skinId)
        {
            return SkinData.FirstOrDefault(data => data.SkinId == skinId);
        }

        public PlayerSkinData GetDefaultSkin()
        {
            return SkinData.FirstOrDefault(data => data.IsPurchasedByDefault && data.IsDefaultSkin);
        }

        private int GetDefaultSkinsCount()
        {
            return SkinData.Count(data => data.IsPurchasedByDefault && data.IsDefaultSkin);
        }

        private void OnValidate()
        {
#if UNITY_EDITOR
            var i = 0;
            foreach (var playerSkinData in SkinData)
            {
                if(!playerSkinData.PlayerSkinPrefab)
                    continue;
                
                if(playerSkinData.PlayerSkinPrefab.SkinId == playerSkinData.SkinId)
                    continue;
                
                playerSkinData.PlayerSkinPrefab.SkinId = playerSkinData.SkinId;
                EditorUtility.SetDirty(playerSkinData.PlayerSkinPrefab);
                i++;
            }
            
            if(i > 0)
                EditorUtility.SetDirty(this);
#endif

        }
    }
}