using UnityEngine;

namespace GamersGrotto.Core.ScriptablePreferences.PreferenceTypes
{
    [CreateAssetMenu(menuName = Constants.ScriptablePreferencesPath + "String ScriptablePreference", fileName = "StringScriptablePreference")]
    public class StringScriptablePreference : ScriptablePreference
    {
        [SerializeField] private string defaultValue;

        [HideInInspector] public string value;
        
        public override void Load()
        {
            if(string.IsNullOrEmpty(Key))
                return;
            
            value = PlayerPrefs.GetString(Key, defaultValue); 
        }

        public override void Save()
        {
            if(string.IsNullOrEmpty(Key))
                return;
            
            PlayerPrefs.SetString(Key, value);
            PlayerPrefs.Save();
        }
    }
}