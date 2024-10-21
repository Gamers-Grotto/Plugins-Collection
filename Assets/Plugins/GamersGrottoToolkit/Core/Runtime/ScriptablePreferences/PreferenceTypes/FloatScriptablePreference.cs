using UnityEngine;

namespace GamersGrotto.Core.ScriptablePreferences.PreferenceTypes
{
    [CreateAssetMenu(menuName = Constants.ScriptablePreferencesPath + "Float ScriptablePreference", fileName = "FloatScriptablePreference")]
    public class FloatScriptablePreference : ScriptablePreference
    {
        [SerializeField] private float defaultValue;

        [HideInInspector] public float value;
        
        public override void Load()
        {
            if(string.IsNullOrEmpty(Key))
                return;
            
            value = PlayerPrefs.GetFloat(Key, defaultValue); 
        }

        public override void Save()
        {
            if(string.IsNullOrEmpty(Key))
                return;
            
            PlayerPrefs.SetFloat(Key, value);
            PlayerPrefs.Save();
        }
    }
}