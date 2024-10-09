namespace GamersGrotto.Runtime.Modules.DamageSystem {
    public class AdditiveDamageModifier: ValueModifier {
        /// <summary>
        /// Returns the amount added by the modifier.
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public override float ApplyModifier(float amount) => amount + modifier;
    }
}