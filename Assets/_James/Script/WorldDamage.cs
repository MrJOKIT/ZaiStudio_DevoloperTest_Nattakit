using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _James.Script.GoogleSheet;
using UnityEngine;

public enum AttackType
{
    NormalAttack,
    SmallAttack,
    PowerThrow,
    DoubleAttack,
}
public class WorldDamage : MonoBehaviour
{
    public DataBase dataBase;
    
    [SerializeField] private float smallDamage = 5;
    [SerializeField] private float normalDamage = 8;
    [SerializeField] private float powerDamage = 10;
    [SerializeField] private float doubleDamage = 5;

    private void Awake()
    {
        var data = dataBase.Content.DamageData;
        smallDamage = data.First(x => x.Name == AttackType.SmallAttack).Damage;
        normalDamage = data.First(x => x.Name == AttackType.NormalAttack).Damage;
        powerDamage = data.First(x => x.Name == AttackType.PowerThrow).Damage;
        doubleDamage = data.First(x => x.Name == AttackType.DoubleAttack).Damage;
    }

    public float GetSmallDamage() { return smallDamage; }
    public float GetNormalDamage() { return normalDamage; }
    public float GetPowerDamage() { return powerDamage; }
    public float GetDoubleDamage() { return doubleDamage; }
}
