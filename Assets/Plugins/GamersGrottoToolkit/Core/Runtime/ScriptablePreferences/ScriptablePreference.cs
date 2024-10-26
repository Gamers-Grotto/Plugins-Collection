using UnityEngine;

namespace GamersGrotto.Core.ScriptablePreferences
{
    public abstract class ScriptablePreference : ScriptableObject
    {
        [field: SerializeField] public string Key { get; private set; }

        protected void OnDisable() => Save();

        public abstract void Load();
        public abstract void Save();
    }
}