using System;
using UnityEngine;

namespace Platforms
{
    public class PlatformSpawnPoint : MonoBehaviour
    {
        [SerializeField] private bool _debug = true;
        [SerializeField] private Color _debugColor = Color.green;
        [SerializeField] private Vector3 _debugScale = Vector3.one;

        private void OnDrawGizmos()
        {
#if UNITY_EDITOR
            
            if (!_debug)
                return;

            var cube = Resources.GetBuiltinResource<Mesh>("Cube.fbx");
            Gizmos.color = _debugColor;
            Gizmos.DrawMesh(cube, transform.position, transform.rotation,
                Vector3.Scale(transform.lossyScale, _debugScale));

#endif
        }

        public bool IsInBounds(Vector2 bounds)
        {
            if (transform.position.x < -bounds.x)
                return false;
            if (transform.position.x > bounds.x)
                return false;
            if (transform.position.z < -bounds.y)
                return false;
            if (transform.position.z > bounds.y)
                return false;

            return true;
        }
    }
}