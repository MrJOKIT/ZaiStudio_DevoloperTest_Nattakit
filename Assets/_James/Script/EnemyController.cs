using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyDifficulty
{
    Easy,
    Normal,
    Hard,
}
public class EnemyController : MonoBehaviour, IUnit
{
    public PlayerNumber enemyNumber;
    [Header("Enemy Setting")]
    public EnemyDifficulty enemyDifficulty;
    [Range(0,100f)][SerializeField] private float missChance = 50f;
    public void StartTurn()
    {
        PrepareShootPlayer();
    }

    public void EndTurn()
    {
        GetComponent<EnemyProjectileThrower>().ResetTarget();
        GameManager.instance.GetComponent<TurnManager>().EndUnitTurn();
    }

    private void PrepareShootPlayer()
    {
        float randomNumber = Random.Range(0f, 100f);
        if (randomNumber >= missChance)
        {
            //Miss Attack
            GetComponent<EnemyProjectileThrower>().FireProjectile(this);
        }
        else
        {
            //Success Attack
            GetComponent<EnemyProjectileThrower>().FireProjectile(this);
        }
    }
}
