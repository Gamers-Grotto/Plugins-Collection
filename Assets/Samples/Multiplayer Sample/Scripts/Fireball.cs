using System;
using Unity.Netcode;
using UnityEngine;

namespace GamersGrotto.Multiplayer_Sample
{
    public class Fireball : NetworkBehaviour
    {
        public float damage = 10f;
        
        private void OnCollisionEnter(Collision other)
        {
            if(!IsServer)
                return;
            
            if (other.gameObject.TryGetComponent<NetworkHealth>(out var health))
            {
                health.TakeDamageServerRpc(damage);
            }
            
            GetComponent<NetworkObject>().Despawn(true);
        }
    }
}