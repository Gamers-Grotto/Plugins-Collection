using UnityEngine;

namespace GamersGrotto.Core.ScriptableVariables.VariableTypes
{
    [CreateAssetMenu(fileName = "NewBoolVariable", menuName = Constants.ScriptableVariablePath + "Bool Variable")]
    public class BoolScriptableVariable : ScriptableVariable<bool>
    {
        public override void ApplyChange(bool value) => Value = Value && value;
    }
}