using UnityEngine;

namespace GamersGrotto.Runtime.Modules.DamageSystem {
    [CreateAssetMenu(fileName = "NewDamage", menuName = "GamersGrotto/Damage System/DamageSO")]
    public class DamageSO: ScriptableObject {
        [SerializeField] private float damage;
        
        public float Damage => damage;
    }
}