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

        [ServerRpc]
        public void TakeDamage(float damage)
        {
            health.Value = Mathf.Clamp(health.Value - damage, 0, maxHealth);
        }
        
        #region Testing

        [ContextMenu("Test")]
        public void TestDamage()
        {
            if(!IsOwner)
                return;
            TakeDamage(Random.Range(1, 15));
        }

        #endregion
    }
}