using Attributes;
using UnityEngine;

namespace GamersGrotto.Runtime.Modules.DamageSystem {
    [CreateAssetMenu(fileName = "NewDamage", menuName = "GamersGrotto/Damage System/DamageSO")]
    public class DamageSO : ScriptableObject {
        [SerializeField] float damage;

        [SerializeField, Optional, Range(0, 1)]
        float critChance = 0f;

        [SerializeField, Optional, Min(1f), Tooltip("Only used if successful crit")]
        float critMultiplier = 1.5f;

        [SerializeField, Optional] DamageType damageType = DamageType.None;
        public float Damage => damage;
        public float CritChance => critChance;
        public float CritMultiplier => critMultiplier;
        public DamageType DamageType => damageType;


        public bool IsCrit() {
            return Random.value < critChance;
        }
    }
}