using GamersGrotto.Core;
using GamersGrotto.Damage_System;
using UnityEngine;

public class CollisionDamageDealer : MonoBehaviour
{
    [SerializeField] private DamageComponent damageComponent;
    [SerializeField] private LayerMask enemyLayer;
    void Awake() {
       damageComponent = GetComponent<DamageComponent>();
    }

    void OnCollisionEnter(Collision other) {
        if (!other.gameObject.IsInLayerMask(enemyLayer))
            return;
        
        var health = other.gameObject.GetComponent<Health>();
        if (health == null) {
            Debug.LogWarning($"[{gameObject.name}] Tried to deal damage to [{other.gameObject.name}] but it does not have a Health component.", this);
            return;
        }
        damageComponent.DealDamage(health);
    }
}
