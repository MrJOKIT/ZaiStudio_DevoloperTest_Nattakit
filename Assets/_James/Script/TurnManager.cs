using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _James.Script.GoogleSheet;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerNumber
{
    Player1,
    Player2,
}

public enum TimerState
{
    Stopped,
    Started,
}
public class TurnManager : MonoBehaviour
{
    [Header("Turn Setting")]
    public PlayerNumber playerTurnState = PlayerNumber.Player1;
    [Space(20)] 
    [Header("Turn Timer")] 
    public TimerState timerState = TimerState.Stopped;
    public DataBase dataBase;
    [SerializeField] private float timeToThink = 30f;
    [SerializeField] private float timeToWarning = 10f;
    private float timeCounter = 30f;
    [SerializeField] private GameObject timerWarningUIOne;
    [SerializeField] private GameObject timerWarningUITwo;
    [SerializeField] private Image timerImageOne;
    [SerializeField] private Image timerImageTwo;

    private void Awake()
    {
        timeToThink = dataBase.Content.SettingData.First(x=> x.Name == "Time to think").Sec;
        timeToWarning = dataBase.Content.SettingData.First(x=> x.Name == "Time to Warning").Sec;
    }

    private void Start()
    {
        StartUnitTurn();
    }
    public void EndUnitTurn()
    {
        playerTurnState = playerTurnState == PlayerNumber.Player1 ? PlayerNumber.Player2 : PlayerNumber.Player1;

        StartUnitTurn();
    }
    
    public void StartUnitTurn()
    {
        if (playerTurnState == PlayerNumber.Player1)
        {
            GetComponent<GameManager>().playerOne.GetComponent<IUnit>().StartTurn();
        }
        else
        {
            GetComponent<GameManager>().RandomWindForce();
            if (GetComponent<GameManager>().GameMode == GameMode.PVP)
            {
                GetComponent<GameManager>().playerTwo.GetComponent<IUnit>().StartTurn();
            }
            else
            {
                GetComponent<GameManager>().enemy.GetComponent<IUnit>().StartTurn();
            }
        }
        
        StartTimer();
    }

    private void Update()
    {
        if (GetComponent<GameManager>().IsGameOver)
        {
            return;
        }
        if (timerState == TimerState.Stopped)
        {
            return;
        }

        if (timeCounter > 0)
        {
            timeCounter -= Time.deltaTime;
            if (timeCounter <= timeToWarning)
            {
                if (playerTurnState == PlayerNumber.Player1)
                {
                    timerWarningUIOne.SetActive(true);
                    timerImageOne.fillAmount = timeCounter / timeToWarning;
                }
                else
                {
                    timerWarningUITwo.SetActive(true);
                    timerImageTwo.fillAmount = timeCounter / timeToWarning;
                }
            }
        }
        else
        {
            StopTimer();
            
            if (playerTurnState == PlayerNumber.Player1)
            {
                GetComponent<GameManager>().playerOne.GetComponent<IUnit>().EndTurn();
            }
            else
            {
                if (GetComponent<GameManager>().GameMode == GameMode.PVP)
                {
                    GetComponent<GameManager>().playerTwo.GetComponent<IUnit>().EndTurn();
                }
                else
                {
                    GetComponent<GameManager>().enemy.GetComponent<IUnit>().EndTurn();
                }
            }
            
            
        }
        
    }

    private void StartTimer()
    {
        timerWarningUIOne.SetActive(false);
        timerWarningUITwo.SetActive(false);
        timeCounter = timeToThink;
        timerState = TimerState.Started;
    }

    public void StopTimer()
    {
        timerState = TimerState.Stopped;
        timerWarningUIOne.SetActive(false);
        timerWarningUITwo.SetActive(false);
    }
}
