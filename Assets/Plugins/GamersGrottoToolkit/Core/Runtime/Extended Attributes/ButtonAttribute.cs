using System;
using UnityEngine;

namespace GamersGrotto.Core.Extended_Attributes {
    [AttributeUsage(AttributeTargets.Method)]
    public class ButtonAttribute : PropertyAttribute
    {
        public string ButtonText { get; private set; }

        public ButtonAttribute(string buttonText = null)
        {
            ButtonText = buttonText;
        }
    }
}
