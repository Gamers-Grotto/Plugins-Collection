using UnityEngine;

namespace GamersGrotto.Core.ScriptableVariables
{
    public abstract class ScriptableVariable<T> : ScriptableObject
    {
        [field: SerializeField] public T Value { get; protected set; }

        public virtual void SetValue(T value) => Value = value;

        public virtual void SetValue(ScriptableVariable<T> value) => Value = value.Value;
        
        public abstract void ApplyChange(T value);
        
        public virtual void ApplyChange(ScriptableVariable<T> value) => ApplyChange(value.Value);
    }
}