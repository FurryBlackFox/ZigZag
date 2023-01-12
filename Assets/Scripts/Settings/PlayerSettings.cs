using Player;
using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "PlayerSettings", menuName = "App/PlayerSettings", order = 0)]
    public class PlayerSettings : ScriptableObject
    {
        [field: SerializeField]
        public PlayerMovement.Direction InitialMoveDirection { get; private set; } = PlayerMovement.Direction.Right;
        [field:SerializeField] public float DefaultMovementSpeed { get; private set; } = 5f;
        
        [field:SerializeField] public LayerMask GroundDetectionLayers { get; private set; }
        [field: SerializeField] public float GroundDetectionRange { get; private set; } = 10f;
    }
}