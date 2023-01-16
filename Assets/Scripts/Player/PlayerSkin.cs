using Sirenix.OdinInspector;
using UnityEngine;

namespace Player
{
    public class PlayerSkin : MonoBehaviour
    {
        [field: SerializeField, ReadOnly] public int SkinId { get; set; } = 0;
    }
}