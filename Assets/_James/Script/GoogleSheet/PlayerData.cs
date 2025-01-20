using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerData", fileName = "Scriptable/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    public float playerHealth;
    public float heal;
    
    [ContextMenu("SyncData")]
    private void SyncData()
    {
        
    }
}
