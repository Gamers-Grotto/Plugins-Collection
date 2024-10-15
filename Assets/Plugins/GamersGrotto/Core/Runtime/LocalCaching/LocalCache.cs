using UnityEngine;

namespace GamersGrotto.LocalCaching
{
    public abstract class LocalCache : ScriptableObject
    {
        [field: SerializeField] public string Key { get; private set; }

        public abstract void Load();
        public abstract void Save();
    }
}