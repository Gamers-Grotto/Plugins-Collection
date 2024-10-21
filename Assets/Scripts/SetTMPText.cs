using TMPro;
using UnityEngine;

namespace GamersGrotto
{
    public class SetTMPText : MonoBehaviour
    {
        private TMP_Text text;
        
        private void Awake() => text = GetComponent<TMP_Text>();

        public void SetText(int value) => text.SetText(value.ToString());

        public void SetText(float value) => text.SetText(value.ToString("0.0"));
    }
}
