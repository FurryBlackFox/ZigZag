using Sirenix.OdinInspector;
using UI.SkinShopButtons;
using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "UISettings", menuName = "App/UISettings", order = 0)]
    public class UISettings : ScriptableObject
    {
        [field: SerializeField, Required, TitleGroup("Skin Shop")]
        public SkinShopButton SkinShopButton { get; private set; }
    }
}