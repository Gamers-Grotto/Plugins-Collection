using UnityEngine;

namespace GamersGrotto.ScriptablePreferences.PreferenceTypes
{
    [CreateAssetMenu(menuName = "GamersGrotto/Scriptable Preferences/String ScriptablePreference", fileName = "StringScriptablePreference")]
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