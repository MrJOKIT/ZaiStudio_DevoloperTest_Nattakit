using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EnemyData", fileName = "Scriptable/EnemyData", order = 1)]
public class EnemyData : ScriptableObject
{
    public EnemyDifficulty enemyDifficulty;
    public float enemyMaxHealth;
    public float missChance;

    [ContextMenu("SyncData")]
    private void SyncData()
    {
        
    }
}
