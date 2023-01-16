using System;
using Player;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Settings
{
    [Serializable]
    public class PlayerSkinData
    {
        [field: SerializeField] public int SkinId { get; private set; } = 0;
        [field: SerializeField, Required] public PlayerSkin PlayerSkinPrefab { get; private set; }
        [field: SerializeField, Required] public Sprite Sprite { get; private set; }
        
        [field: SerializeField, TitleGroup("Purchasing")] public bool IsPurchasedByDefault { get; private set; } = false;
        [field: SerializeField, ShowIf(nameof(IsPurchasedByDefault))] public bool IsDefaultSkin { get; private set; } = false;
        [field: SerializeField, HideIf(nameof(IsPurchasedByDefault)) ] public int PurchasePrice { get; private set; } = 100;
    }
}