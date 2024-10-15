using UnityEngine;

namespace GamersGrotto.LocalCaching
{
    [CreateAssetMenu(menuName = "GamersGrotto/LocalCache/String LocalCache", fileName = "StringLocalCache")]
    public class StringLocalCache : LocalCache
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