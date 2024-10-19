using System;
using System.IO;
using System.Threading.Tasks;
using GamersGrotto.Save_Game_System.Profiles;
using GamersGrotto.Save_Game_System.SaveSlots;
using GamersGrotto.Save_Game_System.SaveSystems;
using Newtonsoft.Json;
using UnityEngine;

namespace GamersGrotto.Save_Game_System
{
    public class SaveSystem
    {
        public static SaveSlotId ActiveSlotId { get; private set; } = SaveSlotId.Slot00;
        
        private static string _saveDirectory = "SaveGames";
        private static string _saveFileExtension = ".sav";
        
        private ISaveSystem saveSystem;
        
        public SaveSystem(ISaveSystem saveSystem, string saveDirectory = "SaveGames", string saveFileExtension = ".sav")
        {
            this.saveSystem = saveSystem;
            
            _saveDirectory = saveDirectory;
            _saveFileExtension = saveFileExtension;
        }

        public async Task Save<T>(string key, T saveData) 
            => await saveSystem.SaveData(key, JsonConvert.SerializeObject(saveData, Formatting.Indented));

        public async Task Save(string key, string json)
            => await saveSystem.SaveData(key, json);

        public async Task<string> Load(string key)
        {
            if (!SaveExists(ActiveSlotId))
            {
                Debug.LogWarning("SaveSystem: Save file does not exist.");
                return default;
            }
            
            var json = await saveSystem.LoadData(key);
            return json;
        }
        
        public async Task<T> Load<T>(string key)
        {
            try
            {
                if (!SaveExists(ActiveSlotId))
                {
                    Debug.LogWarning("SaveSystem: Save file does not exist.");
                    return default;
                } 
                        
                var json = await Load(key);
                return JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings() { ConstructorHandling = ConstructorHandling.Default});
            }
            catch (Exception e)
            {
                Debug.LogError("SaveSystem : Something went wrong - "+e);
            }
            return default;
        }

        public bool SaveExists(SaveSlotId slotId)
            => saveSystem.SaveExists(slotId);

        public bool HasSaveGames()
        {
            foreach (SaveSlotId saveSlotId in Enum.GetValues(typeof(SaveSlotId)))
            {
                if (SaveExists(saveSlotId))
                    return true;
            }

            return false;
        }

        #region SaveSlots
        public void SetActiveSlot(SaveSlotId slotId)
            => ActiveSlotId = slotId;
        
        public DateTime GetLastModified(SaveSlotId slotId)
            => Directory.GetLastWriteTime(GetSaveFolder(ProfileSelector.ActiveProfile.Name, slotId));

        public void DeleteSaveSlot(SaveSlotId slotId)
        {
            var directory = GetSaveFolder(ProfileSelector.ActiveProfile.Name, slotId);
            
            if(Directory.Exists(directory))
                Directory.Delete(directory, true);
        }
        #endregion
        
        #region SavePath
        public static string GetProfileFolder(string profileName)
            => Path.Combine(Application.persistentDataPath, _saveDirectory, profileName);

        public static string GetSaveFolder(string profileName, SaveSlotId slotId)
            => Path.Combine(GetProfileFolder(profileName), slotId.ToString());

        public static string GetSaveFolderForActiveSaveSlot(string profileName)
            => GetSaveFolder(profileName, ActiveSlotId);

        public static string GetSaveFilePathForActiveSaveSlot(string profileName, string key)
        {
            var saveDirectory = GetSaveFolderForActiveSaveSlot(profileName);
            var saveFilePath = Path.Combine(saveDirectory, key + _saveFileExtension);
            return saveFilePath;
        }
        #endregion
    }
}
