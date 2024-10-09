using UnityEngine;

namespace GamersGrotto.Runtime.Modules.DamageSystem {
    /// <summary>
    /// Separated to its own abstract class to allow for easier sorting between modifiers.
    /// </summary>
    public abstract class ValueModifier : ScriptableObject {
        [Min(0)] public float modifier = 1f;

        /// <summary>
        /// Returns the amount modified by the modifier.
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public abstract float ApplyModifier(float amount);
    }
}