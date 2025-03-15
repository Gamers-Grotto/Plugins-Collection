using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

namespace GamersGrotto.Multiplayer_Sample
{
    public class NetworkHealth : NetworkBehaviour
    {
        [SerializeField] private NetworkVariable<float> health = new (100f, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
        [SerializeField] private float maxHealth;
        
        public UnityEvent<float> onHealthChanged = new ();
        
        [ServerRpc]
        public void TakeDamageServerRpc(float damage)
        {
            if(!IsServer)
                return;
            
            health.Value -= Mathf.Clamp(damage, 0, maxHealth);
            onHealthChanged.Invoke(health.Value / maxHealth);
        }

        [ServerRpc]
        public void HealServerRpc(float heal)
        {
            if(!IsServer) 
                return;
            
            health.Value += Mathf.Clamp(heal, 0, maxHealth);
            onHealthChanged.Invoke(health.Value / maxHealth);
        }
        
        #region Testing

        [ContextMenu("Test")]
        public void TestDamage()
        {
            if(!IsServer) 
                return;
            
            TakeDamageServerRpc(Random.Range(1, 15));
        }

        #endregion
    }
}