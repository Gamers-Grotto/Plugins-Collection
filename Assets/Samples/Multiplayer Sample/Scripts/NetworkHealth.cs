using System;
using GamersGrotto.GG_Broker;
using GamersGrotto.Multiplayer_Sample.Messages;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace GamersGrotto.Multiplayer_Sample
{
    public class NetworkHealth : NetworkBehaviour
    {
        [SerializeField] private NetworkVariable<float> health = new (100f, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
        [SerializeField] private float maxHealth;
        
        public UnityEvent<float> onHealthChanged = new ();

        private void OnEnable()
        {
            health.OnValueChanged += OnNetworkHealthChanged;
        }

        private void OnDisable()
        {
            health.OnValueChanged -= OnNetworkHealthChanged;
        }

        private void OnNetworkHealthChanged(float previousvalue, float newvalue)
        {
            Debug.Log($"Network Health changed on {OwnerClientId} : from {previousvalue} to {newvalue}");
            onHealthChanged.Invoke(newvalue / maxHealth);
        }
        
        [ServerRpc(RequireOwnership = false)]
        public void TakeDamageServerRpc(float damage, ulong attackerClientId)
        {
            health.Value = Mathf.Clamp(health.Value - damage, 0, maxHealth);
            
            if(health.Value <= 0)
            {
                Debug.Log($"Player {OwnerClientId} has died");
                new PlayerDeathMessage(OwnerClientId, attackerClientId).Invoke();
            }
        }
        
        #region Testing
        
            [ContextMenu("Test ServerRPC Damage")]
            public void TestDamageServer()
            {
                TakeDamageServerRpc(Random.Range(1, 15), OwnerClientId);
            }
            [ContextMenu("Suicide")]
            public void Suicide()
            {
                TakeDamageServerRpc(100, OwnerClientId);
            }
            
        #endregion
    }
}