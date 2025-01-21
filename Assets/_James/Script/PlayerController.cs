using System;
using System.Collections;
using System.Collections.Generic;
using _James.Script.GoogleSheet;
using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;


public enum ShootState
{
    PrepareShoot,
    OnShooting,
    ShootSuccess,
}
public class PlayerController : MonoBehaviour,IUnit
{
    [Header("Player Information")]
    public PlayerNumber currentPlayerNumber;
    public ShootState shootState;
    public GameObject playerCanvas;
    private ProjectileThrow projectileThrow;

    [Space(20)] 
    [Header("Player Status")] 
    public PlayerData playerData;
    [SerializeField] private float playerHealth;
    [SerializeField] private float playerMaxHealth = 50f;
    [SerializeField] private float healPoint;
    public Image healthBar;
    public bool onMouseOver;
    
    [Header("Animation")]
    public SkeletonAnimation skeletonAnimation;
    
    public float PlayerHealth {get{return playerHealth;}}
    

    public void InitializePlayer(float playerMaxHealth, float healPoint)
    {
        this.playerMaxHealth = playerMaxHealth;
        this.healPoint = healPoint;
        
        projectileThrow = GetComponent<ProjectileThrow>();
        playerHealth = playerMaxHealth;
        UpdateHealthUI();
    }

    private void Update()
    {
        if (GameManager.instance.IsGameOver)
        {
            return;
        }
        if (shootState == ShootState.ShootSuccess || currentPlayerNumber != GameManager.instance.GetComponent<TurnManager>().playerTurnState)
        {
            return;
        }
        if (Input.GetMouseButton(0) && onMouseOver || Input.GetMouseButton(0) && shootState == ShootState.OnShooting)
        {
            shootState = ShootState.OnShooting;
            projectileThrow.projectileSpeed += Time.deltaTime * 15;
            if (projectileThrow.projectileSpeed > projectileThrow.GetEndProjectileSpeed)
            {
                projectileThrow.FireProjectile(this);
                shootState = ShootState.ShootSuccess;
            }
        }

        if (Input.GetMouseButtonUp(0) && onMouseOver || Input.GetMouseButtonUp(0) && shootState == ShootState.OnShooting)
        { 
            projectileThrow.FireProjectile(this);
            shootState = ShootState.ShootSuccess;
        }
    }

    private void FixedUpdate()
    {
        GetComponent<Collider2D>().enabled = currentPlayerNumber == GameManager.instance.GetComponent<TurnManager>().playerTurnState;
    }

    public void StartTurn()
    {
        shootState = ShootState.PrepareShoot;
        playerCanvas.SetActive(true);
    }

    public void EndTurn()
    {
        playerCanvas.SetActive(false);
        skeletonAnimation.AnimationState.SetAnimation(0, "Idle Friendly 1", true);
        if (GetComponent<ProjectileThrow>().currentPower == PowerList.DoubleAttack)
        {
            GetComponent<ProjectileThrow>().currentPower = PowerList.None;
            projectileThrow.FireProjectile(this);
        }
        else
        {
            GetComponent<ProjectileThrow>().currentPower = PowerList.None;
            GetComponent<ProjectileThrow>().ResetTarget();
            GameManager.instance.GetComponent<TurnManager>().EndUnitTurn();
        }
    }

    public void PlayHitAnimation()
    {
        skeletonAnimation.AnimationState.SetAnimation(0, "Happy Friendly", true);
        if (GameManager.instance.IsGameOver)
        {
            return;
        }
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

    public void TakeDamage(float damage)
    {
        if (SoundManager.instance != null)
        {
            SoundManager.instance.PlaySound(SoundName.Hit);
        }
        
        playerHealth -= damage;
        if (playerHealth <= 0)
        {
            playerHealth = 0;
            PlayLoseAnimation();
            GameManager.instance.GameOver();
        }
        else
        {
            StartCoroutine(PlayHurtAnimation());
        }
        UpdateHealthUI();
    }

    public void TakeHeal()
    {
        playerHealth += healPoint;
        if (playerHealth > playerMaxHealth)
        {
            playerHealth = playerMaxHealth;
        }
        UpdateHealthUI();
    }
    private void UpdateHealthUI()
    {
        healthBar.fillAmount = playerHealth / playerMaxHealth;
    }

    private void OnMouseEnter()
    {
        onMouseOver = true;
    }

    private void OnMouseExit()
    {
        onMouseOver = false;
    }

    
}
