using System;
using GamersGrotto.Core.ScriptableVariables.VariableTypes;
using TMPro;
using UnityEngine;

namespace GamersGrotto
{
    public class SetTMPText : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;
        
        private void Awake()
        {
            if(text == null)
                text = GetComponent<TMP_Text>();
        }

        public void SetText(string value) => text.SetText(value);

        public void SetText(StringScriptableVariable soVariable) => SetText(soVariable.Value);
        public void SetText(IntScriptableVariable soVariable) => SetText(soVariable.Value.ToString());
        public void SetText(FloatScriptableVariable soVariable) => SetText(soVariable.Value.ToString("0.0"));
        public void SetText(BoolScriptableVariable soVariable) => SetText(soVariable.Value.ToString());
        
    }
}
