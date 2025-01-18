using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerNumber
{
    Player1,
    Player2,
}
public class TurnManager : MonoBehaviour
{
    
    public PlayerNumber playerTurnState = PlayerNumber.Player1;
    public List<IUnit> unitList;

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
            if (GetComponent<GameManager>().GameMode == GameMode.PVP)
            {
                GetComponent<GameManager>().playerTwo.GetComponent<IUnit>().StartTurn();
            }
            else
            {
                GetComponent<GameManager>().enemy.GetComponent<IUnit>().StartTurn();
            }
        }
    }
}
