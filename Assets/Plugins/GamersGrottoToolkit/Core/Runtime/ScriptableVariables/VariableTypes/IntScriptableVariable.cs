using UnityEngine;

namespace GamersGrotto.Core.ScriptableVariables.VariableTypes
{
    [CreateAssetMenu(fileName = "NewIntVariable", menuName = ScriptableVariablePath + "Int Variable")]
    public class IntScriptableVariable : ScriptableVariable<int>
    {
        public override void ApplyChange(int value) => Value += value;
    }
}