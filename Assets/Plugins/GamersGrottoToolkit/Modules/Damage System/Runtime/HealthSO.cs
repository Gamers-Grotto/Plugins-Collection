using System.Collections.Generic;
using GamersGrotto.Damage_System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewHealthSO", menuName = "GamersGrotto/Damage System/HealthSO")]
public class HealthSO : ScriptableObject {
    [SerializeField] private float baseMax = 100f;
    [SerializeField] private List<DamageType> baseResistances = new List<DamageType>();
    [SerializeField] private List<DamageType> baseWeaknesses = new List<DamageType>();
    [SerializeField] private List<DamageType> baseImmunities = new List<DamageType>();
    
    public float BaseMax => baseMax;
    public List<DamageType> BaseResistances => baseResistances;
    public List<DamageType> BaseWeaknesses => baseWeaknesses;
    public List<DamageType> BaseImmunities => baseImmunities;
    
}