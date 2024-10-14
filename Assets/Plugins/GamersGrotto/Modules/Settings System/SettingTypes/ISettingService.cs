using UnityEngine;

namespace GamersGrotto.Modules.Settings_System
{
    public interface ISettingService
    {
        void Load();
        void Apply();
        void Save();
    }
    
    public class AudioSettingService : ScriptableObject, ISettingService
    {
        
        
        public void Load()
        {
            throw new System.NotImplementedException();
        }

        public void Apply()
        {
            throw new System.NotImplementedException();
        }

        public void Save()
        {
            throw new System.NotImplementedException();
        }
    }
}