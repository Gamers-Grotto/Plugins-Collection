using System.Threading.Tasks;
using GamersGrotto.Runtime.Modules.SaveGameSystem.SaveSlots;

namespace GamersGrotto.Runtime.Modules.SaveGameSystem.SaveSystems
{
    public interface ISaveSystem //I don't like the name of this interface...perhaps, ISaveSource? ISaveResource?
    {
        bool SaveExists(SaveSlotId slotId);
        Task SaveData(string key, string json);
        Task<string> LoadData(string key);
        void DeleteSave(string key);
    }
}