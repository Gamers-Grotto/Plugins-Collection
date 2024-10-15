using UnityEngine;

namespace GamersGrotto.LocalCaching
{
    [CreateAssetMenu(menuName = "GamersGrotto/LocalCache/Vector3 LocalCache", fileName = "Vector3LocalCache")]
    public class Vector3LocalCache : LocalCache
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