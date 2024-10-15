using UnityEngine;

namespace GamersGrotto.ScriptablePreferences
{
    public abstract class ScriptablePreference : ScriptableObject
    {
        [field: SerializeField] public string Key { get; private set; }

        public abstract void Load();
        public abstract void Save();
    }
}