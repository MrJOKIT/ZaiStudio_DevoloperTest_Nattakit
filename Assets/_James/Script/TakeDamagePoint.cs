using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamagePoint : MonoBehaviour
{
    public PlayerController player;

    private void Update()
    {
        GetComponent<Collider2D>().enabled = player.currentPlayerNumber != GameManager.instance.GetComponent<TurnManager>().playerTurnState;
    }
}
