using UnityEngine;

namespace GamersGrotto.Modules.Settings_System
{
    [CreateAssetMenu(menuName = "GamersGrotto/Settings/Boolean Setting", fileName = "BoolSetting")]
    public class BoolSetting : Setting
    {
        public bool defaultValue;

        [HideInInspector] public bool value;
        
        public override void Load()
        {
            value = PlayerPrefs.GetInt(Key, defaultValue ? 1 : 0) == 1; 
        }

        public override void Save()
        {
            PlayerPrefs.SetInt(Key, value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }
}