using UnityEngine;

namespace GamersGrotto.ScriptablePreferences.PreferenceTypes
{
    [CreateAssetMenu(menuName = "GamersGrotto/Scriptable Preferences/Int ScriptablePreference", fileName = "IntScriptablePreference")]
    public class IntScriptablePreference : ScriptablePreference
    {
        [SerializeField] private int defaultValue;

        [HideInInspector] public int value;
        
        public override void Load()
        {
            if(string.IsNullOrEmpty(Key))
                return;
            
            value = PlayerPrefs.GetInt(Key, defaultValue); 
        }

        public override void Save()
        {
            if(string.IsNullOrEmpty(Key))
                return;
            
            PlayerPrefs.SetInt(Key, value);
            PlayerPrefs.Save();
        }
    }
}