using System;
using Unity.Netcode;
using UnityEngine;

namespace GamersGrotto.Multiplayer_Sample
{
    public class Fireball : NetworkBehaviour
    {
        public float damage = 10f;

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log($"IsLocalPlayer ({GetType().Name}): {IsLocalPlayer}");
            if(!IsServer)
                return;
            
            if (other.gameObject.TryGetComponent<NetworkHealth>(out var health))
            {
                health.TakeDamageServerRpc(damage, OwnerClientId);
            }
            
            GetComponent<NetworkObject>().Despawn(true);
        }
    }
}