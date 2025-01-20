using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public void SelectModePVP()
    {
        GameModeManager.instance.gameMode = GameMode.PVP;
    }
    public void SelectModePVE()
    {
        GameModeManager.instance.gameMode = GameMode.PVE;
    }
    
    public void SelectEasyDifficulty()
    {
        GameModeManager.instance.enemyDifficulty = EnemyDifficulty.Easy;
    }
    public void SelectNormalDifficulty()
    {
        GameModeManager.instance.enemyDifficulty = EnemyDifficulty.Normal;
    }
    public void SelectHardDifficulty()
    {
        GameModeManager.instance.enemyDifficulty = EnemyDifficulty.Hard;
    }
}
