using System;
using System.Collections;
using System.Collections.Generic;
using Spine.Unity;
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
    public bool isDoubleAttackUse;
    [Header("Enemy UI")]
    public Image healthBar;
    public SkeletonAnimation skeletonAnimation;
    
    public float EnemyHealth
    {
        get { return enemyHealth; }
    }
    

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
        skeletonAnimation.AnimationState.SetAnimation(0, "Idle Friendly 1", true);
        if (isDoubleAttackUse)
        {
            GetComponent<EnemyProjectileThrower>().EnemyFireProjectile(this,EnemyAttackType.Double);
            isDoubleAttackUse = false;
        }
        else
        {
            GameManager.instance.GetComponent<TurnManager>().EndUnitTurn();
        }
        
    }

    public void PlayHitAnimation()
    {
        skeletonAnimation.AnimationState.SetAnimation(0, "Happy Friendly", true);
        StartCoroutine(EndTurnDelay());
    }

    public void PlayMissAnimation()
    {
        skeletonAnimation.AnimationState.SetAnimation(0, "Moody Friendly", true);
        StartCoroutine(EndTurnDelay());
    }

    public void PlayWinAnimation()
    {
        skeletonAnimation.AnimationState.SetAnimation(0, "Cheer Friendly", true);
    }

    public void PlayLoseAnimation()
    {
        skeletonAnimation.AnimationState.SetAnimation(0, "Moody UnFriendly", true);
    }

    public IEnumerator PlayHurtAnimation()
    {
        skeletonAnimation.AnimationState.SetAnimation(0, "Moody UnFriendly", true);
        yield return new WaitForSeconds(2f);
        skeletonAnimation.AnimationState.SetAnimation(0, "Idle Friendly 1", true);
    }

    public void PlayDogeAnimation()
    {
        skeletonAnimation.AnimationState.SetAnimation(0, "Sleep Friendly", true);
        StartCoroutine(EndTurnDelay());
    }
    
    IEnumerator EndTurnDelay()
    {
        yield return new WaitForSeconds(2);
        EndTurn();
    }

    private void ShootPlayer()
    {
        float randomNumber = Random.Range(0f, 100f);
        if (randomNumber >= missChance)
        {
            //Miss Attack
            GetComponent<EnemyProjectileThrower>().EnemyFireProjectile(this,EnemyAttackType.Normal);
        }
        else
        {
            //Success Attack
            GetComponent<EnemyProjectileThrower>().EnemyFireProjectile(this,EnemyAttackType.Perfect);
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
            PlayLoseAnimation();
            GameManager.instance.GameOver();
        }
        else
        {
            StartCoroutine(PlayHurtAnimation());
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
