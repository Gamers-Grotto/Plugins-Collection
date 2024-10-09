using UnityEngine;

namespace GamersGrotto.Runtime.Modules.DamageSystem {
    /// <summary>
    /// Separated to its own abstract class to allow for easier sorting between modifiers.
    /// </summary>
    public abstract class ValueModifier : ScriptableObject {
        [Min(0)] public float multiplier = 1f;

        /// <summary>
        /// Returns the amount multiplied by the modifier.
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public virtual float ApplyModifier(float amount) {
            return amount * multiplier;
        }
    }
}