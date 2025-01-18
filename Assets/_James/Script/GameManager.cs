using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameMode
{
    PVP,
    PVE,
}
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private GameMode gameMode;
    [SerializeField] private float currentWindForce;

    [Header("Character")] 
    public GameObject playerOne;
    public GameObject playerTwo;
    public GameObject enemy;
    public float WindForce { get { return currentWindForce; }}
    public GameMode GameMode { get { return gameMode; }}

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        
        SetUpGame();
    }

    private void SetUpGame()
    {
        if (gameMode == GameMode.PVP)
        {
            enemy.SetActive(false);
            playerTwo.SetActive(true);
        }
        else if (gameMode == GameMode.PVE)
        {
            playerOne.SetActive(false);
            enemy.SetActive(true);
        }
    }
}
