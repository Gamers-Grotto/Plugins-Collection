using UnityEngine;

namespace GamersGrotto.LocalCaching
{
    [CreateAssetMenu(menuName = "GamersGrotto/LocalCache/Int LocalCache", fileName = "IntLocalCache")]
    public class IntLocalCache : LocalCache
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