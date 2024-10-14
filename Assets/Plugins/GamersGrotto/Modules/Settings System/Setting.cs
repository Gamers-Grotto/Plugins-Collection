using UnityEngine;

namespace GamersGrotto.Modules.Settings_System
{
    public abstract class Setting : ScriptableObject
    {
        [field: SerializeField] public string Key { get; private set; }

        public abstract void Load();
        public abstract void Save();
    }
}