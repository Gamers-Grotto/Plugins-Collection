using UnityEngine;

namespace GamersGrotto.Modules.Settings_System
{
    [CreateAssetMenu(menuName = "GamersGrotto/Settings/String Setting", fileName = "StringSetting")]
    public class StringSetting : Setting
    {
        public string defaultValue;

        [HideInInspector] public string value;
        
        public override void Load()
        {
            value = PlayerPrefs.GetString(Key, defaultValue); 
        }

        public override void Save()
        {
            PlayerPrefs.SetString(Key, value);
            PlayerPrefs.Save();
        }
    }
}