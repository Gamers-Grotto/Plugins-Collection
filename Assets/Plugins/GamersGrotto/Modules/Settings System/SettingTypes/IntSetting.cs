using UnityEngine;
using UnityEngine.Serialization;

namespace GamersGrotto.Modules.Settings_System
{
    [CreateAssetMenu(menuName = "GamersGrotto/Settings/Int Setting", fileName = "IntSetting")]
    public class IntSetting : Setting
    {
        public int defaultValue;

        [HideInInspector] public int value;
        
        public override void Load()
        {
            value = PlayerPrefs.GetInt(Key, defaultValue); 
        }

        public override void Save()
        {
            PlayerPrefs.SetInt(Key, value);
            PlayerPrefs.Save();
        }
    }
}