using GamersGrotto.Runtime.Modules.DamageSystem;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDamageModifier", menuName = "GamersGrotto/Damage System/DamageModifier")]
public class MultiplicativeDamageModifier : ValueModifier {
    public override float ApplyModifier(float amount) {
        return amount * modifier;
    }
}