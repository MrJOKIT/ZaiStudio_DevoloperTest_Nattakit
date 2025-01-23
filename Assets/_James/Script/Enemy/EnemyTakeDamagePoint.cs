using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTakeDamagePoint : MonoBehaviour
{
    public EnemyController enemy;
    private void Update()
    {
        GetComponent<Collider2D>().enabled = enemy.enemyNumber != GameManager.instance.GetComponent<TurnManager>().playerTurnState;
    }
}
