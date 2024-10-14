using UnityEngine;

namespace GamersGrotto.Modules.Settings_System
{
    [CreateAssetMenu(menuName = "GamersGrotto/Settings/Float Setting", fileName = "FloatSetting")]
    public class FloatSetting : Setting
    {
        public float defaultValue;

        [HideInInspector] public float value;
        
        public override void Load()
        {
            value = PlayerPrefs.GetFloat(Key, defaultValue); 
        }

        public override void Save()
        {
            PlayerPrefs.SetFloat(Key, value);
            PlayerPrefs.Save();
        }
    }
}