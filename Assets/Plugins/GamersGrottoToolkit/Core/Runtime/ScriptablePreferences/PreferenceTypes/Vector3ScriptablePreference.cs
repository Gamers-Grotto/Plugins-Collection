using UnityEngine;

namespace GamersGrotto.Core.ScriptablePreferences.PreferenceTypes
{
    [CreateAssetMenu(menuName = "GamersGrotto/Scriptable Preferences/Vector3 ScriptablePreference", fileName = "Vector3ScriptablePreference")]
    public class Vector3ScriptablePreference : ScriptablePreference
    {
        [SerializeField] private Vector3 defaultValue;

        [HideInInspector] public Vector3 value;
        public override void Load()
        {
            if(string.IsNullOrEmpty(Key))
                return;
            
            value = JsonUtility.FromJson<Vector3>(PlayerPrefs.GetString(Key, JsonUtility.ToJson(defaultValue)));
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