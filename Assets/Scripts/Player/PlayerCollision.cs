using System;
using UnityEngine;

namespace Player
{
    public class PlayerCollision : MonoBehaviour
    {
        public event Action OnDeathZoneTriggerEntered; 
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("DeathZone"))
                OnDeathZoneTriggerEntered?.Invoke();
        }
    }
}