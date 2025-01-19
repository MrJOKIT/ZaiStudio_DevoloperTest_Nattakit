using System;
using System.Collections;
using System.Collections.Generic;
using Spine;
using UnityEngine;
using UnityEngine.UI;

public enum PowerList
{
    None,
    PowerThrow,
    DoubleAttack,
}
public class ProjectileThrow : MonoBehaviour
{
    [Header("References")]
    public Transform shootPoint;
    public GameObject projectile;
    public GameObject powerProjectile;
    [Space(20)]
    [Header("Projectile Setting")]
    [SerializeField] private float startProjectileSpeed = 15;
    [SerializeField] private float endProjectileSpeed = 50;
    public float GetEndProjectileSpeed { get { return endProjectileSpeed; } }

    [Space(20)] 
    [Header("Current Speed")] 
    public Image shootBar;
    public float projectileSpeed;

    [Space(20)] 
    [Header("Projectile Ability")]
    public PowerList currentPower;
    public GameObject powerThrowButton;
    public GameObject doubleAttackButton;
    public GameObject healButton;

    private void Awake()
    {
        projectileSpeed = startProjectileSpeed;
    }

    private void Update()
    {
        shootBar.fillAmount = (projectileSpeed - 15) / (endProjectileSpeed - 15);
    }
    
    public void FireProjectile(IUnit host)
    {
        
        if (currentPower == PowerList.PowerThrow)
        {
            var projectileObject = Instantiate(powerProjectile,shootPoint.position,shootPoint.rotation);
            Vector2 velocity = shootPoint.up * projectileSpeed;
            projectileObject.GetComponent<ProjectileObject>().host = host;
            projectileObject.GetComponent<Rigidbody2D>().velocity = velocity;
            projectileObject.GetComponent<ProjectileObject>().isPowerThrow = true;
        }
        else
        {
            var projectileObject = Instantiate(projectile,shootPoint.position,shootPoint.rotation);
            Vector2 velocity = shootPoint.up * projectileSpeed;
            projectileObject.GetComponent<ProjectileObject>().host = host;
            projectileObject.GetComponent<Rigidbody2D>().velocity = velocity;
            
            if (currentPower == PowerList.DoubleAttack)
            {
                projectileObject.GetComponent<ProjectileObject>().isDoubleAttack = true;
            }
            
        }

        if (currentPower != PowerList.DoubleAttack)
        {
            projectileSpeed = startProjectileSpeed;
        }
        
        GameManager.instance.GetComponent<TurnManager>().StopTimer();
    }

    public void UsePowerThrow()
    {
        if (currentPower != PowerList.None || GameManager.instance.GetComponent<TurnManager>().playerTurnState != GetComponent<PlayerController>().currentPlayerNumber)
        {
            return;
        }
        currentPower = PowerList.PowerThrow;
        powerThrowButton.SetActive(false);
    }

    public void UseDoubleAttack()
    {
        if (currentPower != PowerList.None || GameManager.instance.GetComponent<TurnManager>().playerTurnState != GetComponent<PlayerController>().currentPlayerNumber)
        {
            return;
        }
        currentPower = PowerList.DoubleAttack;
        doubleAttackButton.SetActive(false);
    }
    
    public void UseHeal()
    {
        if (currentPower != PowerList.None || GameManager.instance.GetComponent<TurnManager>().playerTurnState != GetComponent<PlayerController>().currentPlayerNumber)
        {
            return;
        }
        GetComponent<PlayerController>().TakeHeal(20);
        GetComponent<IUnit>().EndTurn();
        healButton.SetActive(false);
    }
}
