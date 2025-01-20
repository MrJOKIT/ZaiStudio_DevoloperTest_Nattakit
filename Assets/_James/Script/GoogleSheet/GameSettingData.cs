using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SettingData", fileName = "Scriptable/SettingData", order = 1)]
public class GameSettingData : ScriptableObject
{
    public float timeToThink;
    public float timeToWarning;
    
    [ContextMenu("SyncData")]
    private void SyncData()
    {
        
    }
}
