using UnityEngine;
using UnityEngine.Events;
using GamersGrotto.Core;
using GamersGrotto.Core.Extended_Attributes;
using GamersGrotto.Core.ScriptableVariables.VariableTypes;

public class Spike : MonoBehaviour
{
    [SerializeField, ShowInInspector] private FloatScriptableVariable spikeDamage;
    [SerializeField] private LayerMask enemyLayer;
    
    public UnityEvent<float> collisionWithSpike;
    
    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.IsInLayerMask(enemyLayer)) 
            return;
        
        collisionWithSpike?.Invoke(spikeDamage.Value);
    }
}