using UnityEngine;

namespace GamersGrotto.ScriptablePreferences.PreferenceTypes
{
    [CreateAssetMenu(menuName = "GamersGrotto/Scriptable Preferences/Vector2 ScriptablePreference", fileName = "Vector2ScriptablePreference")]
    public class Vector2ScriptablePreference : ScriptablePreference
    {
        [SerializeField] private Vector2 defaultValue;

        [HideInInspector] public Vector2 value;
        public override void Load()
        {
            if(string.IsNullOrEmpty(Key))
                return;
            
            value = JsonUtility.FromJson<Vector2>(PlayerPrefs.GetString(Key, JsonUtility.ToJson(defaultValue)));
        }

        public override void Save()
        {
            if(string.IsNullOrEmpty(Key))
                return;
            
            PlayerPrefs.SetString(Key, JsonUtility.ToJson(value));
            PlayerPrefs.Save();
        }
    }
}