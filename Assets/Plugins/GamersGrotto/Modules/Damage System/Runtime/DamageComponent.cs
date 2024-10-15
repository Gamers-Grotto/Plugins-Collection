using System.Collections.Generic;
using Attributes;
using UnityEngine;

namespace GamersGrotto.Runtime.Modules.DamageSystem {
    /// <summary>
    /// The purpose of this component is to allow something to deal damage to an object with a Health component.
    /// </summary>
    public class DamageComponent : MonoBehaviour {
        public DamageSO damageSO;

        public List<ValueModifier> damageModifiers = new List<ValueModifier>();

        public void DealDamage(Health health) {
            var damage = damageSO.Damage;

            //Apply crit if applicable
            bool isCrit = damageSO.IsCrit();
            if (isCrit) {
                damage *= damageSO.CritMultiplier;
            }

            //Apply all damage modifiers if any
            foreach (var modifier in damageModifiers) {
                damage = modifier.ApplyModifier(damage);
            }

            health.TakeDamage(damage, isCrit, damageSO.DamageType);
        }

        #region Debugging

        [Button]
        public void TestAttackDebugTarget() {
            
            if(!Application.isPlaying)
            {
                Debug.LogWarning("Enter Play Mode to Test Attacking");
                return;
            }
            
            var damage = damageSO.Damage;
            
            //Apply crit if applicable
            bool isCrit = damageSO.IsCrit();
            if (isCrit) {
                damage *= damageSO.CritMultiplier;
            }

            //Apply all damage modifiers if any
            foreach (var modifier in damageModifiers) {
                damage = modifier.ApplyModifier(damage);
            }

            var objectToAdd = new GameObject();
            objectToAdd.AddComponent<Health>();
            objectToAdd.name = $"Debug Target of [{gameObject.name}]";
            objectToAdd.GetComponent<Health>().TakeDamage(damage, isCrit, damageSO.DamageType);
            Destroy(objectToAdd, 5f);
            Debug.Log($"[{gameObject.name}] Modified damage: {damage}. Crit? ({isCrit}). Damage Type ({damageSO.DamageType})", this);
        }

        #endregion
    }
}