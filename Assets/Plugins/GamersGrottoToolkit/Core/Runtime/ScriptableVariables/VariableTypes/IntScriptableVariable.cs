using UnityEngine;

namespace GamersGrotto.Core.ScriptableVariables.VariableTypes
{
    [CreateAssetMenu(fileName = "NewIntVariable", menuName = Constants.ScriptableVariablePath + "Int Variable")]
    public class IntScriptableVariable : ScriptableVariable<int>
    {
        public override void ApplyChange(int value) => Value += value;

        public void SetValue(IntScriptableVariable intSOVariable) => SetValue(intSOVariable.Value);

        public void ApplyChange(IntScriptableVariable intSOVariable) => ApplyChange(intSOVariable.Value);
    }
}