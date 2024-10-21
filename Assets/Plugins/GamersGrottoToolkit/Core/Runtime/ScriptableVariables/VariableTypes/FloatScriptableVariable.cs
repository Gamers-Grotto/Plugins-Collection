using UnityEngine;

namespace GamersGrotto.Core.ScriptableVariables.VariableTypes
{
    [CreateAssetMenu(fileName = "NewFloatVariable", menuName = Constants.ScriptableVariablePath + "Float Variable")]
    public class FloatScriptableVariable : ScriptableVariable<float>
    {
        public override void ApplyChange(float value) => Value += value;
    }
}