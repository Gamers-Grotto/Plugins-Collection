using System.Collections.Generic;

namespace GamersGrotto.Modules.Settings_System
{
    public class SettingsManager : Singleton<SettingsManager>
    {
        public SettingsCollection audioSettings = new ();
        public SettingsCollection graphicsSettings = new();
        public SettingsCollection controlsSettings = new();
        public SettingsCollection languageSettings = new();
        
        public List<Setting> otherSettings;
        
        protected override void Awake()
        {
            base.Awake();
            LoadAllSettings();
        }

        [Button]
        public void LoadAllSettings()
        {
            LoadFromCollection(audioSettings);
            LoadFromCollection(graphicsSettings);
            LoadFromCollection(controlsSettings);
            LoadFromCollection(languageSettings);
            
            foreach (var setting in otherSettings)
                setting.Load();
        }

        [Button]
        public void SaveAllSettings()
        {
            SaveCollection(audioSettings);
            SaveCollection(graphicsSettings);
            SaveCollection(controlsSettings);
            SaveCollection(languageSettings);
            
            foreach (var setting in otherSettings)
                setting.Save();
        }

        private void LoadFromCollection(SettingsCollection collection)
        {
            foreach (var setting in collection.settings)
                setting.Load();
        }

        private void SaveCollection(SettingsCollection collection)
        {
            foreach (var setting in collection.settings)
                setting.Save();
        }

        private void OnApplicationQuit()
        {
            SaveAllSettings();
        }
    }
}