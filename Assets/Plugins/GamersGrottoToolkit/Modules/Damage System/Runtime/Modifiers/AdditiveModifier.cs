using UnityEngine;

namespace GamersGrotto.Damage_System.Modifiers {
    [CreateAssetMenu(fileName = "NewAdditiveModifier", menuName = "GamersGrotto/Damage System/AdditiveModifier")]
    public class AdditiveModifier : ValueModifier {
        /// <summary>
        /// Returns the amount added by the modifier.
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public override float ApplyModifier(float amount) => amount + modifier;
    }
}