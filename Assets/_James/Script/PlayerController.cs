using System;
using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private float playerHealth;
    [SerializeField] private float playerMaxHealth = 50f;
    public Image healthBar;
    public bool onMouseOver;
    
    private void Awake()
    {
        projectileThrow = GetComponent<ProjectileThrow>();
        playerHealth = playerMaxHealth;
        UpdateHealthUI();
    }

    private void Update()
    {
        GetComponent<Collider2D>().enabled = currentPlayerNumber == GameManager.instance.GetComponent<TurnManager>().playerTurnState;
        
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
    
    public void StartTurn()
    {
        shootState = ShootState.PrepareShoot;
        playerCanvas.SetActive(true);
    }

    public void EndTurn()
    {
        playerCanvas.SetActive(false);
        
        if (GetComponent<ProjectileThrow>().currentPower == PowerList.DoubleAttack)
        {
            GetComponent<ProjectileThrow>().currentPower = PowerList.None;
            projectileThrow.FireProjectile(this);
        }
        else
        {
            GetComponent<ProjectileThrow>().currentPower = PowerList.None;
            GameManager.instance.GetComponent<TurnManager>().EndUnitTurn();
        }
    } 

    public void TakeDamage(float damage)
    {
        playerHealth -= damage;
        if (playerHealth <= 0)
        {
            playerHealth = 0;
        }
        UpdateHealthUI();
    }

    public void TakeHeal(float heal)
    {
        playerHealth += heal;
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
