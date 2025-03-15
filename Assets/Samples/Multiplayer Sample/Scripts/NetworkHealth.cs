using GamersGrotto.Core.Extended_Attributes;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

namespace GamersGrotto.Multiplayer_Sample
{
    public class NetworkHealth : NetworkBehaviour
    {
        [SerializeField] private NetworkVariable<float> health;
        [SerializeField] private float maxHealth;
        
        public UnityEvent<float> onHealthChanged = new ();
        
        public void TakeDamage(float damage)
        {
            health.Value -= damage;
            onHealthChanged.Invoke(health.Value / maxHealth);
        }

        public void Heal(float heal)
        {
            health.Value += heal;
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