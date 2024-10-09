using System.Collections.Generic;
using UnityEngine;

namespace GamersGrotto.Runtime.Modules.DamageSystem {
    /// <summary>
    /// The purpose of this component is to allow something to deal damage to an object with a Health component.
    /// </summary>
    public class DamageComponent : MonoBehaviour {
        public DamageSO damageSO;

        public List<DamageModifier> damageModifiers = new List<DamageModifier>();

        public void DealDamage(Health health) {
            var damage = damageSO.Damage;

            foreach (var modifier in damageModifiers) {
                damage = modifier.ApplyModifier(damage);
            }

            health.TakeDamage(damage);
        }

        #region Debugging

        [Button]
        public void PrintModifiedDamage() {
            var damage = damageSO.Damage;

            foreach (var modifier in damageModifiers) {
                damage = modifier.ApplyModifier(damage);
            }

            Debug.Log($"[{gameObject.name}] Modified damage: {damage}.", this);
        }

        #endregion
    }
}
