using System.Collections.Generic;
using GamersGrotto.Core.Extended_Attributes;
using GamersGrotto.Damage_System.Modifiers;
using UnityEngine;

namespace GamersGrotto.Damage_System {
    /// <summary>
    /// The purpose of this component is to allow something to deal damage to an object with a Health component.
    /// It does not handle when the damage should be dealth or to whom, that has to be handled by another script.
    /// </summary>
    public class DamageComponent : MonoBehaviour {
        [ShowInInspector] public DamageSO damageSO;
        [ShowInInspector] public List<ValueModifier> damageModifiers = new List<ValueModifier>();
        [ShowInInspector] public List<ValueModifier> critChanceModifiers = new List<ValueModifier>();


        public void DealDamage(Health health) {
            var damage = damageSO.Damage;

            var isCrit = ApplyCriticalHit(ref damage);

            ApplyDamageModifiers(ref damage);

            health.TakeDamage(damage, isCrit, damageSO.DamageType);
            
            Debug.Log($"[{gameObject.name}] Dealt {damage} damage to [{health.gameObject.name}]. Crit? ({isCrit}). Damage Type ({damageSO.DamageType})", this);
        }

        void ApplyDamageModifiers(ref float damage) {
            foreach (var modifier in damageModifiers) {
                damage = modifier.ApplyModifier(damage);
            }
        }

        bool ApplyCriticalHit(ref float damage) {
            var critChance = CalculateCritChance();
            bool isCrit = RollCrit(critChance);
            if (isCrit) {
                damage *= damageSO.CritMultiplier;
            }

            return isCrit;
        }

        bool RollCrit(float critChance) => Random.value <= critChance;

        float CalculateCritChance() {
            var critChance = damageSO.CritChance;
            foreach (var modifier in critChanceModifiers) {
                critChance = modifier.ApplyModifier(critChance);
            }
            critChance = Mathf.Clamp01(critChance);
            return critChance;
        }

        #region Debugging

        [Button]
        public void TestAttackDebugTarget() {
            if (!Application.isPlaying) {
                Debug.LogWarning("Enter Play Mode to Test Attacking");
                return;
            }

            var damage = damageSO.Damage;

            //Apply crit if applicable
            bool isCrit = RollCrit(CalculateCritChance());
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
            Debug.Log(
                $"[{gameObject.name}] Modified damage: {damage}. Crit? ({isCrit}). Damage Type ({damageSO.DamageType})",
                this);
        }

        #endregion
    }
    
}
