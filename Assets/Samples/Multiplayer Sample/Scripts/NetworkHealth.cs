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

        public void TakeDamage(float damage)
        {
            health.Value = Mathf.Clamp(health.Value - damage, 0, maxHealth);
            onHealthChanged.Invoke(health.Value / maxHealth);
        }

        public void Heal(float heal)
        {
            health.Value = Mathf.Clamp(health.Value + heal, 0, maxHealth);
            onHealthChanged.Invoke(health.Value / maxHealth);
        }
        
        #region Testing

        [ContextMenu("Test")]
        public void TestDamage()
        {
            TakeDamage(Random.Range(1, 15));
        }

        #endregion
    }
}