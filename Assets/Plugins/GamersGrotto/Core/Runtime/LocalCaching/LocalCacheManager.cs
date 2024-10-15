using System.Collections.Generic;

namespace GamersGrotto.LocalCaching
{
    public class LocalCacheManager : Singleton<LocalCacheManager>
    {
        public List<LocalCache> caches;
        
        protected override void Awake()
        {
            base.Awake();
            LoadAll();
        }

        private void OnApplicationQuit()
        {
            SaveAll();
        }

        [Button]
        public void SaveAll()
        {
            foreach (var cache in caches)
                cache.Save();
        }
        
        [Button]
        public void LoadAll()
        {
            foreach (var cache in caches)
                cache.Load();
        }
    }
}