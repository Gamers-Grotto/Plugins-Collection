using GamersGrotto.Runtime.Modules.DamageSystem;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDamageModifier", menuName = "GamersGrotto/Damage System/DamageModifier")]
public class MultiplicativeDamageModifier : ValueModifier {
    
    /// <summary>
    /// Returns the amount multiplied by the modifier.
    /// </summary>
    /// <param name="amount"></param>
    /// <returns></returns>
    public override float ApplyModifier(float amount) {
        return amount * modifier;
    }
}