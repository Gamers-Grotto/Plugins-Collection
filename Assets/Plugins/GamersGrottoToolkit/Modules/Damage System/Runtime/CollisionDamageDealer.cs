using System.Collections.Generic;
using GamersGrotto.Core;
using GamersGrotto.Damage_System;
using UnityEngine;

public class CollisionDamageDealer : MonoBehaviour
{
    [SerializeField] private DamageComponent damageComponent;
    [SerializeField] protected List<LayerMask> enemyLayers;
    void Awake() {
       damageComponent = GetComponent<DamageComponent>();
    }

    void OnCollisionEnter(Collision other) {
        var enemyInLayer = false;
        foreach (var enemyLayer in enemyLayers) {
            if (other.gameObject.IsInLayerMask(enemyLayer)) {
                enemyInLayer = true;
                break;
            }
        }
        
        if (!enemyInLayer)
            return;
        var health = other.gameObject.GetComponent<Health>();
        if (health == null) {
            Debug.LogWarning($"[{gameObject.name}] Tried to deal damage to [{other.gameObject.name}] but it does not have a Health component.", this);
            return;
        }
        damageComponent.DealDamage(health);

        DoChildLogic(other);
    }
    
    /// <summary>
    /// Is called after the damage has been dealt to the object.
    /// </summary>
    /// <param name="other"></param>
    public virtual void DoChildLogic(Collision other) {
        //This is a virtual method that can be overridden by child classes for added functionality
    }
}
