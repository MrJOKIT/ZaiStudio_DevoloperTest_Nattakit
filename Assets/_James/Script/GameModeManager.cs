using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeManager : MonoBehaviour
{
    public static GameModeManager instance;
    public GameMode gameMode;
    public EnemyDifficulty enemyDifficulty;
    
    [Header("PlayerInfo")]
    public Sprite playerSprite;
    public string playerName;
    public string playerId;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void InitializePlayerInfo(string playerName, string playerId)
    {
        this.playerSprite = playerSprite;
        this.playerName = playerName;
        this.playerId = playerId;
    }
}
