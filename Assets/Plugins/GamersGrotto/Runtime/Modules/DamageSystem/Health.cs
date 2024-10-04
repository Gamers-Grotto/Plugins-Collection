using UnityEngine;
using UnityEngine.Events;

namespace GamersGrotto.Runtime.Modules.DamageSystem
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float baseHealth = 100f;
        [SerializeField] private float max = 100f;
        [field: SerializeField] public bool Invulnerable { get; private set; }
       
        public UnityEvent<float, float> healthChanged;
        public UnityEvent death;
        
        public float BaseHp => baseHealth;
        
        private float _current;
        public float Current
        {
            get => _current;
            private set
            {
                if(Mathf.Approximately(_current, value))
                    return;
                
                _current = Mathf.Clamp(value, 0, Max);
                healthChanged?.Invoke(_current, Max);
            }
        }
    
        public float Max
        { 
            get => max;
            private set
            {
                if(Mathf.Approximately(max, value))
                    return;
                
                max = Mathf.Clamp(value, 1, float.MaxValue);
                healthChanged?.Invoke(Current, max);
            }
        }
        
        public bool IsFull => Current >= Max;
        public bool IsDead => Current <= 0 ;
        public float LastDamaged { get; private set; }
        
        public void Initialize()
        {
            SetMaxHealth(BaseHp);
            Current = BaseHp;
        }
        
        public void Initialize(float current, float max)
        {
            SetMaxHealth(max);
            Current = current;
        }

        public void SetInvulnerability(bool invulnerable) => Invulnerable = invulnerable;

        public void SetMaxHealth(float newMax)
        {
            if (newMax <= 0)
                return;
        
            Max = newMax;
            
            if(Current > Max)
                Current = newMax;
        }

        public void Heal(float healing)
        {
            if(healing <= 0)
                return;
        
            if(IsDead)
                return;

            Current += healing;
        }

        public void TakeDamage(float damage)
        {
            if(Invulnerable)
                return;
            
            if(damage <= 0)
                return;
        
            if (IsDead)
                return;
        
            Current -= damage;
            LastDamaged = Time.time;
        
            if(IsDead)
                death?.Invoke();
        }

        #region Debug

        [ContextMenu("Test Damage")]
        public void TestDamage()
        {
            TakeDamage(10f);
        }

        #endregion
    }
}