using UnityEngine;

namespace GamersGrotto.LocalCaching
{
    [CreateAssetMenu(menuName = "GamersGrotto/LocalCache/Float LocalCache", fileName = "FloatLocalCache")]
    public class FloatLocalCache : LocalCache
    {
        [SerializeField] private float defaultValue;

        [HideInInspector] public float value;
        
        public override void Load()
        {
            if(string.IsNullOrEmpty(Key))
                return;
            
            value = PlayerPrefs.GetFloat(Key, defaultValue); 
        }

        public override void Save()
        {
            if(string.IsNullOrEmpty(Key))
                return;
            
            PlayerPrefs.SetFloat(Key, value);
            PlayerPrefs.Save();
        }
    }
}