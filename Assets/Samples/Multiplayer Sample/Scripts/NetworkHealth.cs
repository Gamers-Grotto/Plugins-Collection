using System;
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
        
        [ClientRpc]
        public void TakeDamageClientRpc(float damage)
        {
            health.Value = Mathf.Clamp(health.Value - damage, 0, maxHealth);
        }
        
        [ServerRpc]
        public void TakeDamageServerRpc(float damage)
        {
            health.Value = Mathf.Clamp(health.Value - damage, 0, maxHealth);
        }
        
        #region Testing

        [ContextMenu("Test ClientRPC Damage")]
        public void TestDamageClient()
        {
            TakeDamageClientRpc(Random.Range(1, 15));
        }
        
        [ContextMenu("Test ServerRPC Damage")]
        public void TestDamageServer()
        {
            TakeDamageServerRpc(Random.Range(1, 15));
        }

        #endregion
    }
}