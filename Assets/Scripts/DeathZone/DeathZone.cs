using UnityEngine;

namespace DeathZone
{
    [RequireComponent(typeof(Collider))]
    public class DeathZone : MonoBehaviour
    {
        [SerializeField] private Collider _collider;
    

        private void OnValidate()
        {
            if (_collider == null)
                _collider = GetComponent<Collider>();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            var colliderBounds = _collider.bounds;
            Gizmos.DrawWireCube(colliderBounds.center, 2 * colliderBounds.extents);
        }
    }
}
