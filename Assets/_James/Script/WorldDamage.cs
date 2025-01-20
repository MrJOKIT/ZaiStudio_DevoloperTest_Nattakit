using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldDamage : MonoBehaviour
{
    public DamageData damageData;
    
    [SerializeField] private float smallDamage = 5;
    [SerializeField] private float normalDamage = 8;
    [SerializeField] private float powerDamage = 10;
    [SerializeField] private float doubleDamage = 5;

    private void Awake()
    {
        smallDamage = damageData.smallDamage;
        normalDamage = damageData.normalDamage;
        powerDamage = damageData.powerDamage;
        doubleDamage = damageData.doubleDamage;
    }

    public float GetSmallDamage() { return smallDamage; }
    public float GetNormalDamage() { return normalDamage; }
    public float GetPowerDamage() { return powerDamage; }
    public float GetDoubleDamage() { return doubleDamage; }
}
