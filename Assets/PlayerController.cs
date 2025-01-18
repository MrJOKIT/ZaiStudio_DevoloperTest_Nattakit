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
    
    private void Awake()
    {
        projectileThrow = GetComponent<ProjectileThrow>();
        playerHealth = playerMaxHealth;
        UpdateHealthUI();
    }

    private void Update()
    {
        if (shootState == ShootState.ShootSuccess || currentPlayerNumber != GameManager.instance.GetComponent<TurnManager>().playerTurnState)
        {
            return;
        }
        if (Input.GetMouseButton(0))
        {
            shootState = ShootState.OnShooting;
            projectileThrow.projectileSpeed += Time.deltaTime * 15;
            if (projectileThrow.projectileSpeed > projectileThrow.GetEndProjectileSpeed)
            {
                projectileThrow.FireProjectile(this);
                shootState = ShootState.ShootSuccess;
            }
        }

        if (Input.GetMouseButtonUp(0))
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
        GameManager.instance.GetComponent<TurnManager>().EndUnitTurn();
    }

    public void UpdateHealthUI()
    {
        healthBar.fillAmount = playerHealth / playerMaxHealth;
    }
}
