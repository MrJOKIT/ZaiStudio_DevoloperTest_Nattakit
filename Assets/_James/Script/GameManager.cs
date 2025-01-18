using System;
using System.Collections;
using System.Collections.Generic;
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
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private GameMode gameMode;
    [Space(20)]
    [Header("Wind Force Setting")]
    [SerializeField] private float currentWindForce;
    [SerializeField] private float maxWindForce = 20f;
    [SerializeField] private AreaEffector2D areaEffector2D;
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
    public GameObject enemy;
    //public float WindForce { get { return currentWindForce; }}
    
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
        
        windSide = WindSide.None;
        areaEffector2D.forceMagnitude = 0;
        
        UpdateWindForceUI();
    }

    public void RandomWindForce()
    {
        float randomNumber = Random.Range(0f, 1f);
        if (randomNumber < 0.45f)
        {
            areaEffector2D.forceAngle = 180f;
            windSide = WindSide.LeftSide;
            currentWindForce = Random.Range(0f,maxWindForce);
            areaEffector2D.forceMagnitude = currentWindForce;
        }
        else if (randomNumber > 0.55f)
        {
            areaEffector2D.forceAngle = 0f;
            windSide = WindSide.RightSide;
            currentWindForce = Random.Range(0f,maxWindForce);
            areaEffector2D.forceMagnitude = currentWindForce;
        }
        else
        {
            windSide = WindSide.None;
            currentWindForce = 0;
            areaEffector2D.forceMagnitude = currentWindForce;
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
}
