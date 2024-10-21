using UnityEngine;

namespace GamersGrotto.Core.ScriptableVariables.VariableTypes
{
    [CreateAssetMenu(fileName = "NewStringVariable", menuName = Constants.ScriptableVariablePath + "String Variable")]
    public class StringScriptableVariable : ScriptableVariable<string>
    {
        /// <summary>
        /// this will append the string to the current value. 
        /// </summary>
        public override void ApplyChange(string value) => Value += value;
    }
}