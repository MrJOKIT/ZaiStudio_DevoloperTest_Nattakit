using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class EnemyController : MonoBehaviour, IUnit
{
    public EnemyData enemyData;
    public PlayerNumber enemyNumber;
    [Header("Enemy Stats")] 
    [SerializeField] private float enemyHealth;
    [SerializeField] private float enemyMaxHealth;

    [Header("Enemy Setting")] 
    [Range(0,100f)][SerializeField] private float missChance = 50f;
    [Header("Enemy UI")]
    public Image healthBar;
    

    private void Start()
    {
        enemyHealth = enemyMaxHealth;
        UpdateHealthUI();
    }

    public void SetUpEnemy(EnemyData enemyData)
    {
        this.enemyData = enemyData;
        enemyMaxHealth = enemyData.enemyMaxHealth;
        missChance = enemyData.missChance;
    }
    public void StartTurn()
    {
        ShootPlayer();
    }

    public void EndTurn()
    {
        GetComponent<EnemyProjectileThrower>().ResetTarget();
        GameManager.instance.GetComponent<TurnManager>().EndUnitTurn();
    }

    private void ShootPlayer()
    {
        float randomNumber = Random.Range(0f, 100f);
        if (randomNumber >= missChance)
        {
            //Miss Attack
            GetComponent<EnemyProjectileThrower>().EnemyFireProjectile(this,false);
        }
        else
        {
            //Success Attack
            GetComponent<EnemyProjectileThrower>().EnemyFireProjectile(this,true);
        }
    }
    
    private void UpdateHealthUI()
    {
        healthBar.fillAmount = enemyHealth / enemyMaxHealth;
    }
    public void TakeDamage(float damage)
    {
        enemyHealth -= damage;
        if (enemyHealth <= 0)
        {
            enemyHealth = 0;
            GameManager.instance.GameOver();
        }
        UpdateHealthUI();
    }

    public void TakeHeal(float heal)
    {
        enemyHealth += heal;
        if (enemyHealth > enemyMaxHealth)
        {
            enemyHealth = enemyMaxHealth;
        }
        UpdateHealthUI();
    }
}
