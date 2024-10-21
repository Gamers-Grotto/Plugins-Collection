using GamersGrotto.Core.Extended_Attributes;
using UnityEngine;

namespace GamersGrotto.Damage_System {
    [CreateAssetMenu(fileName = "NewDamage", menuName = "GamersGrotto/Damage System/DamageSO")]
    public class DamageSO : ScriptableObject {
        [SerializeField] float baseMin, baseMax;

        [SerializeField, Optional, Range(0, 1)]
        float critChance = 0f;

        [SerializeField, Optional, Min(1f), Tooltip("Only used if successful crit")]
        float critMultiplier = 1.5f;

        [SerializeField, Optional] DamageType damageType = DamageType.None;
        public float Damage => Random.Range(baseMin, baseMax);
        public float CritChance => critChance;
        public float CritMultiplier => critMultiplier;
        public DamageType DamageType => damageType;
    }
}