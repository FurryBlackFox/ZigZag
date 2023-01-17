using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using Vfx;

namespace Settings
{
    [CreateAssetMenu(fileName = "VfxSettings", menuName = "App/VfxSettings", order = 0)]
    public class VfxSettings : ScriptableObject
    {
        [field: TitleGroup("Jewel Collected Announcers")]
        [field: SerializeField]
        public bool EnableOnJewelCollectedAnnouncers { get; private set; } = true;
        
        [field: SerializeField, Required, ShowIf(nameof(EnableOnJewelCollectedAnnouncers))]
        public JewelCollectedAnnouncer JewelCollectedAnnouncerPrefab { get; private set; }

        [field: SerializeField, ShowIf(nameof(EnableOnJewelCollectedAnnouncers))]
        public float JewelCollectedAnnouncerShowDuration { get; private set; } = 1f;

        [field: SerializeField, ShowIf(nameof(EnableOnJewelCollectedAnnouncers))]
        public float JewelCollectedAnnouncerMoveDistance { get; private set; } = 2f;

        [field: SerializeField, ShowIf(nameof(EnableOnJewelCollectedAnnouncers))]
        public Ease JewelCollectedAnnouncerEase { get; private set; } = Ease.Linear;
    }
}