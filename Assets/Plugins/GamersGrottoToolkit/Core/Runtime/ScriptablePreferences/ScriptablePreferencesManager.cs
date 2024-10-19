using System.Collections.Generic;
using GamersGrotto.Core.Extended_Attributes;

namespace GamersGrotto.Core.ScriptablePreferences
{
    public class ScriptablePreferencesManager : Singleton<ScriptablePreferencesManager>
    {
        public List<ScriptablePreference> preferences;
        
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
            foreach (var pref in preferences)
                pref.Save();
        }
        
        [Button]
        public void LoadAll()
        {
            foreach (var pref in preferences)
                pref.Load();
        }
    }
}