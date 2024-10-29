using UnityEngine;

namespace GamersGrotto.Core.ScriptableVariables
{
    public abstract class ScriptableVariable<T> : ScriptableObject
    {
        [field: SerializeField] public T Value { get; protected set; }

        public virtual void SetValue(T value) => Value = value;
        
        public abstract void ApplyChange(T value);
    }
}