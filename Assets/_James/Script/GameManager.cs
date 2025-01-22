using System;
using System.Collections;
using System.Collections.Generic;
using _James.Script.GoogleSheet;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public enum GameMode
{
    PVP,
    PVE,
}

public enum WindSide
{
    None,
    LeftSide,
    RightSide,
}
public enum EnemyDifficulty
{
    EnemyEasy,
    EnemyNormal,
    EnemyHard,
}
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private GameMode gameMode;
    [SerializeField] private bool isGameOver;
    [Space(20)]
    [Header("Wind Force Setting")]
    [SerializeField] private float currentWindForce;
    [SerializeField] private float maxWindForce = 2.5f;
    [SerializeField] private WindSide windSide;
    [Header("Wind Force UI")] 
    public GameObject leftArrow;
    public GameObject rightArrow;
    public Image leftForceBar;
    public Image rightForceBar;
    
    [Space(20)]
    [Header("Character")] 
    public GameObject playerOne;
    public GameObject playerTwo;
    public GameObject playerTwoItemList;
    [Space(20)]
    [Header("Enemy")]
    public GameObject enemy;
    [Space(20)]
    public EnemyDifficulty enemyDifficulty;

    [Space(20)] 
    [Header("Game Over Setting")]
    public GameObject gameOverPanel;
    public TextMeshProUGUI winText;

    
    //public float WindForce { get { return currentWindForce; }}
    
    public GameMode GameMode { get { return gameMode; }}

    public float CurrentWindForce { get { return currentWindForce; } }
    public WindSide WindSide { get { return windSide; } }
    public bool IsGameOver { get { return isGameOver; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        if (SoundManager.instance != null)
        {
            SoundManager.instance.PlayBGM(BGMName.BGM2);
        }
        
        if (GameModeManager.instance != null)
        {
            gameMode = GameModeManager.instance.gameMode;
            enemyDifficulty = GameModeManager.instance.enemyDifficulty;
        }
        else
        {
            gameMode = GameMode.PVE;
            enemyDifficulty = EnemyDifficulty.EnemyHard;
        }
        
        SetUpGame();
    }

    private void SetUpGame()
    {
        if (gameMode == GameMode.PVP)
        {
            enemy.SetActive(false);
            playerTwo.SetActive(true);
            playerTwoItemList.SetActive(true);
        }
        else if (gameMode == GameMode.PVE)
        {
            playerTwo.SetActive(false);
            playerTwoItemList.SetActive(false);
            enemy.SetActive(true);
            /*switch (enemyDifficulty)
            {
                case EnemyDifficulty.EnemyEasy:
                    enemy.GetComponent<EnemyController>().SetUpEnemy(easyEnemyData);
                    break;
                case EnemyDifficulty.EnemyNormal:
                    enemy.GetComponent<EnemyController>().SetUpEnemy(normalEnemyData);
                    break;
                case EnemyDifficulty.EnemyHard:
                    enemy.GetComponent<EnemyController>().SetUpEnemy(hardEnemyData);
                    break;
            }*/
            
        }
        
        windSide = WindSide.None;
        
        UpdateWindForceUI();
    }

    public void RandomWindForce()
    {
        float randomNumber = Random.Range(0f, 1f);
        if (randomNumber < 0.45f)
        {
            //areaEffector2D.forceAngle = 180f;
            windSide = WindSide.LeftSide;
            currentWindForce = Random.Range(0f,maxWindForce);
            //areaEffector2D.forceMagnitude = currentWindForce;
        }
        else if (randomNumber > 0.55f)
        {
            //areaEffector2D.forceAngle = 0f;
            windSide = WindSide.RightSide;
            currentWindForce = Random.Range(0f,maxWindForce);
            //areaEffector2D.forceMagnitude = currentWindForce;
        }
        else
        {
            windSide = WindSide.None;
            currentWindForce = 0;
            //areaEffector2D.forceMagnitude = currentWindForce;
        }
        
        UpdateWindForceUI();
    }

    private void UpdateWindForceUI()
    {
        switch (windSide)
        {
            case WindSide.None:
                leftArrow.SetActive(false);
                rightArrow.SetActive(false);
                leftForceBar.fillAmount = 0f;
                rightForceBar.fillAmount = 0f;
                break;
            case WindSide.LeftSide:
                leftArrow.SetActive(true);
                rightArrow.SetActive(false);
                leftForceBar.fillAmount = currentWindForce / maxWindForce;
                rightForceBar.fillAmount = 0f;
                break;
            case WindSide.RightSide:
                leftArrow.SetActive(false);
                rightArrow.SetActive(true);
                leftForceBar.fillAmount = 0f;
                rightForceBar.fillAmount = currentWindForce / maxWindForce;
                break;
        }
    }

    public void GameOver()
    {
        if (SoundManager.instance != null)
        {
            SoundManager.instance.PlayBGM(BGMName.BGM3);
        }
        
        gameOverPanel.SetActive(true);
        isGameOver = true;
        if (GameMode == GameMode.PVE)
        {
            if (playerOne.GetComponent<PlayerController>().PlayerHealth > 0)
            {
                playerOne.GetComponent<PlayerController>().PlayWinAnimation();
                winText.text = "You Win!";
            }
            else if (enemy.GetComponent<EnemyController>().EnemyHealth > 0)
            {
                enemy.GetComponent<EnemyController>().PlayWinAnimation();
                winText.text = "You Lose!";
            }
        }
        else
        {
            if (playerOne.GetComponent<PlayerController>().PlayerHealth > 0)
            {
                playerOne.GetComponent<PlayerController>().PlayWinAnimation();
                winText.text = "Player 1 Win!";
            }
            else if ( playerTwo.GetComponent<PlayerController>().PlayerHealth > 0)
            {
                playerTwo.GetComponent<PlayerController>().PlayWinAnimation();
                winText.text = "Player 2 Win!";
            }
        }
    }
}
