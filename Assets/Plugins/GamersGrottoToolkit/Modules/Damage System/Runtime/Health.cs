using System;
using System.Collections.Generic;
using GamersGrotto.Core.Extended_Attributes;
using GamersGrotto.Core.ScriptableVariables.VariableTypes;
using GamersGrotto.Damage_System.Modifiers;
using UnityEngine;
using UnityEngine.Events;

namespace GamersGrotto.Damage_System {
    public class Health : MonoBehaviour {
        [field: SerializeField, ShowInInspector] public HealthSO healthSO;
        float max;
        [field: SerializeField] public bool Invulnerable { get; private set; }

        public List<ValueModifier> maxHealthModifiers = new List<ValueModifier>();
        public UnityEvent<float, float> healthChanged;
        public UnityEvent<float> healthChangedNormalized;
        public UnityEvent death;

        private float _current;

        private const float MIN_MODIFIER_THRESHHOLD = 1f;
        private const float WEAKNESS_MODIFIER = 1.5f;
        private const float RESISTANCE_MODIFIER = 0.5f;

        public float Current {
            get => _current;
            private set {
                if (Mathf.Approximately(_current, value))
                    return;

                _current = Mathf.Clamp(value, 0, Max);
                healthChanged?.Invoke(_current, Max);
                healthChangedNormalized?.Invoke(_current / Max);
            }
        }

        public float Max {
            get => max;
            private set {
                if (Mathf.Approximately(max, value))
                    return;

                max = value;
                Current = Mathf.Clamp(Current, 0, Max);
            }
        }
        
        public bool IsFull => Current >= Max;
        public bool IsDead => Current <= 0;
        public float LastDamaged { get; private set; }

        public void Initialize() {
            UpdateMaxHealth();
            Current = Max;
        }

        void ModifyMaxHealthByModifiers(ref float maxHealth) {
            foreach (var modifier in maxHealthModifiers) {
                maxHealth = modifier.ApplyModifier(maxHealth);
            }
        }

        public void SetInvulnerability(bool invulnerable) => Invulnerable = invulnerable;

        public void UpdateMaxHealth() {
            var newMax = healthSO.BaseMax;
            ModifyMaxHealthByModifiers(ref newMax);

            Max = newMax;
        }

        public void Heal(float healing) {
            if (healing <= 0)
                return;

            if (IsDead)
                return;

            Current += healing;
        }

        public void TakeDamage(float damage) => TakeDamage(damage, false, null);

        public void TakeDamage(FloatScriptableVariable floatVariable) => TakeDamage(floatVariable.Value);

        public void TakeDamage(float damage, bool isCrit, DamageType? damageType = null) {
            if (Invulnerable)
                return;

            if (damage <= 0)
                return;

            if (IsDead)
                return;

            var damageTypeResult = ModifyDamageByType(ref damage, isCrit, damageType);

            Current -= damage;
            LastDamaged = Time.time;

            if (IsDead)
                death?.Invoke();
        }

        DamageTakenTypeResult ModifyDamageByType(ref float damage, bool isCrit, DamageType? damageType) {
            var result = new DamageTakenTypeResult();

            result.IsCrit = isCrit;

            if (damageType == null)
                return result;

            result.HasImmunity = healthSO.BaseImmunities.Contains(damageType.Value);
            result.HasWeakness = healthSO.BaseWeaknesses.Contains(damageType.Value);
            result.HasResistance = healthSO.BaseResistances.Contains(damageType.Value);

            if (result.HasImmunity) {
                damage = 0;
            }
            else if (result.HasWeakness) {
                damage *= WEAKNESS_MODIFIER;
            }
            else if (result.HasResistance) {
                damage *= RESISTANCE_MODIFIER;
            }

            return result;
        }

        public void AddMaxHealthModifier(ValueModifier modifier) {
            maxHealthModifiers.Add(modifier);
            UpdateMaxHealth();
            var newCurrent = modifier.ApplyModifier(Current);
            if (newCurrent > MIN_MODIFIER_THRESHHOLD) {
                Current = newCurrent;
            }
        }

        /// <summary>
        /// 8/10 hp with 0.5x modifier,
        /// when removing the modifier, it should be 16/20 hp.
        /// 8/10 hp with 2x modifier,
        /// when removing the modifier, it should be 4/5 hp.
        /// 8/10 hp with +2 modifier,
        /// when removing the modifier, it should be 6/8 hp.
        /// 8/10 hp with -2 modifier,
        /// when removing the modifier, it should be 10/12 hp.
        /// </summary>
        /// <param name="modifier"></param>
        public void RemoveMaxHealthModifier(ValueModifier modifier) {
            maxHealthModifiers.Remove(modifier);
            UpdateMaxHealth();

            if (modifier is AdditiveModifier additiveModifier) {
                var newCurrent = Current - additiveModifier.modifier;
                if (newCurrent > MIN_MODIFIER_THRESHHOLD) {
                    Current = newCurrent;
                }
            }
            else if (modifier is MultiplicativeModifier multiplicativeModifier) {
                var newCurrent = Current / multiplicativeModifier.modifier;
                if (newCurrent > MIN_MODIFIER_THRESHHOLD) {
                    Current = newCurrent;
                }
            }
        }

        #region Debug

        [ContextMenu("Test Damage")]
        public void TestDamage() {
            TakeDamage(10f, true);
        }

        #endregion
    }

    public struct DamageTakenTypeResult {
        public bool HasImmunity { get; set; }
        public bool HasWeakness { get; set; }
        public bool HasResistance { get; set; }

        public bool IsCrit { get; set; }
    }
}