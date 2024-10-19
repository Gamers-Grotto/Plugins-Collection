using UnityEngine;

namespace GamersGrotto.Damage_System.Modifiers {
    [CreateAssetMenu(fileName = "NewMultiplicativeModifier", menuName = "GamersGrotto/Damage System/MultiplicativeModifier")]
    public class MultiplicativeModifier : ValueModifier {
    
        /// <summary>
        /// Returns the amount multiplied by the modifier.
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public override float ApplyModifier(float amount) {
            return amount * modifier;
        }
    }
}