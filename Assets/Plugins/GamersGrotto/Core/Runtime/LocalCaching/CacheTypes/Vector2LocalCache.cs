using UnityEngine;

namespace GamersGrotto.LocalCaching
{
    [CreateAssetMenu(menuName = "GamersGrotto/LocalCache/Vector2 LocalCache", fileName = "Vector2LocalCache")]
    public class Vector2LocalCache : LocalCache
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