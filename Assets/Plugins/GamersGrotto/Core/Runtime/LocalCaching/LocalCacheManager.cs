using System.Collections.Generic;

namespace GamersGrotto.LocalCaching
{
    public class LocalCacheManager : Singleton<LocalCacheManager>
    {
        public List<LocalCache> cache;
        
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
            foreach (var setting in cache)
                setting.Save();
        }
        
        [Button]
        public void LoadAll()
        {
            foreach (var setting in cache)
                setting.Load();
        }
    }
}