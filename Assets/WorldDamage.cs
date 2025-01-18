using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldDamage : MonoBehaviour
{
    [SerializeField] private float smallDamage = 5;
    [SerializeField] private float normalDamage = 8;
    [SerializeField] private float powerDamage = 10;
    
    public float GetSmallDamage() { return smallDamage; }
    public float GetNormalDamage() { return normalDamage; }
    public float GetPowerDamage() { return powerDamage; }
}
