using UnityEngine;

namespace GamersGrotto.LocalCaching
{
    [CreateAssetMenu(menuName = "GamersGrotto/LocalCache/Boolean LocalCache", fileName = "BoolLocalCache")]
    public class BoolLocalCache : LocalCache
    {
        [SerializeField] private bool defaultValue;

        [HideInInspector] public bool value;
        
        public override void Load()
        {
            if(string.IsNullOrEmpty(Key))
                return;
            
            value = PlayerPrefs.GetInt(Key, defaultValue ? 1 : 0) == 1; 
        }

        public override void Save()
        {
            if(string.IsNullOrEmpty(Key))
                return;
            
            PlayerPrefs.SetInt(Key, value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }
}