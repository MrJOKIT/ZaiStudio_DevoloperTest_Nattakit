using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DamageData", fileName = "Scriptable/DamageData", order = 1)]
public class DamageData : ScriptableObject
{
    public float smallDamage = 5;
    public float normalDamage = 8;
    public float powerDamage = 10;
    public float doubleDamage = 5;

    [ContextMenu("SyncData")]
    private void SyncData()
    {
        
    }
}
