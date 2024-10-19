using System.Threading.Tasks;
using GamersGrotto.Save_Game_System.SaveSlots;

namespace GamersGrotto.Save_Game_System.SaveSystems
{
    public interface ISaveSystem //I don't like the name of this interface...perhaps, ISaveSource? ISaveResource?
    {
        bool SaveExists(SaveSlotId slotId);
        Task SaveData(string key, string json);
        Task<string> LoadData(string key);
        void DeleteSave(string key);
    }
}