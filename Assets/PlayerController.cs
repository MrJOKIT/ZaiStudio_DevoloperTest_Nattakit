using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShootState
{
    PrepareShoot,
    OnShooting,
    ShootSuccess,
}
public class PlayerController : MonoBehaviour,IUnit
{
    public PlayerNumber currentPlayerNumber;
    public ShootState shootState;
    public GameObject playerCanvas;
    private ProjectileThrow projectileThrow;
    
    private void Awake()
    {
        projectileThrow = GetComponent<ProjectileThrow>();
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
            projectileThrow.projectileSpeed += Time.deltaTime * 10;
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
}
