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

public enum ShootSide
{
    LeftSide,
    RightSide,
}
public class ProjectileThrow : MonoBehaviour
{
    [Header("References")] 
    public ShootSide shootSide;
    public Transform shootPoint;
    public Transform shootTarget;
    private Vector3 startTarget;
    [Space(20)]
    [Header("Projectile Setting")]
    [SerializeField] private float startProjectileSpeed = 0;
    [SerializeField] private float endProjectileSpeed = 35;
    public float GetEndProjectileSpeed { get { return endProjectileSpeed; } }

    [Space(20)] 
    [Header("Current Speed")] 
    public Image shootBar;
    public float projectileSpeed;

    [Header("Trajectory Setting")] 
    public float trajectoryMaxSpeed;
    public float trajectoryMaxHeight;
    public AnimationCurve projectileCurve;
    public AnimationCurve axisCorrectionProjectileCurve;
    public AnimationCurve speedProjectileCurve;

    [Space(20)] 
    [Header("Projectile Ability")]
    public PowerList currentPower;
    public GameObject powerThrowButton;
    public GameObject doubleAttackButton;
    public GameObject healButton;

    private void Awake()
    {
        projectileSpeed = startProjectileSpeed;
        startTarget = shootTarget.position;
    }

    private void Update()
    {
        shootBar.fillAmount = (projectileSpeed) / (endProjectileSpeed);

        if (GetComponent<PlayerController>().shootState == ShootState.OnShooting)
        {
            
            if (shootSide == ShootSide.LeftSide)
            {
                if (GameManager.instance.WindSide == WindSide.LeftSide)
                {
                    shootTarget.position += Vector3.left * Time.deltaTime * (7.5f + GameManager.instance.CurrentWindForce);
                }
                else if (GameManager.instance.WindSide == WindSide.RightSide)
                {
                    shootTarget.position += Vector3.left * Time.deltaTime * (7.5f - GameManager.instance.CurrentWindForce);
                }
                else
                {
                    shootTarget.position += Vector3.left * Time.deltaTime * 7.5f;
                }
                
            } 
            else
            { 
                if (GameManager.instance.WindSide == WindSide.LeftSide)
                {
                    shootTarget.position += Vector3.right * Time.deltaTime * (7.5f - GameManager.instance.CurrentWindForce);
                }
                else if (GameManager.instance.WindSide == WindSide.RightSide)
                {
                    shootTarget.position += Vector3.right * Time.deltaTime * (7.5f + GameManager.instance.CurrentWindForce);
                }
                else
                {
                    shootTarget.position += Vector3.right * Time.deltaTime * 7.5f;
                }
            }
           
        }
    }

    public void ResetTarget()
    {
        shootTarget.position = startTarget;
    }
    public void FireProjectile(IUnit host)
    {
        
        if (currentPower == PowerList.PowerThrow)
        {
            if (GetComponent<PlayerController>().currentPlayerNumber == PlayerNumber.Player1)
            {
                GameObject projectilePool = ProjectilePooling.instance.GetBonePowerProjectile();
                if (projectilePool != null) {
                    projectilePool.transform.position = shootPoint.transform.position;
                    projectilePool.transform.rotation = shootPoint.transform.rotation;
                    projectilePool.SetActive(true);
                }
                ProjectileObject projectileObject = projectilePool.GetComponent<ProjectileObject>();
                projectileObject.InitializeProjectile(shootTarget,trajectoryMaxSpeed,trajectoryMaxHeight,currentPower,host);
                projectileObject.InitializeAnimationCurve(projectileCurve,axisCorrectionProjectileCurve,speedProjectileCurve);
            }
            else
            {
                GameObject projectilePool = ProjectilePooling.instance.GetPoopsPowerProjectile();
                if (projectilePool != null) {
                    projectilePool.transform.position = shootPoint.transform.position;
                    projectilePool.transform.rotation = shootPoint.transform.rotation;
                    projectilePool.SetActive(true);
                }
                ProjectileObject projectileObject = projectilePool.GetComponent<ProjectileObject>();
                projectileObject.InitializeProjectile(shootTarget,trajectoryMaxSpeed,trajectoryMaxHeight,currentPower,host);
                projectileObject.InitializeAnimationCurve(projectileCurve,axisCorrectionProjectileCurve,speedProjectileCurve);
            }
            
        }
        else
        {
            if (GetComponent<PlayerController>().currentPlayerNumber == PlayerNumber.Player1)
            {
                GameObject projectilePool = ProjectilePooling.instance.GetBoneProjectile();
                if (projectilePool != null) {
                    projectilePool.transform.position = shootPoint.transform.position;
                    projectilePool.transform.rotation = shootPoint.transform.rotation;
                    projectilePool.SetActive(true);
                }
                ProjectileObject projectileObject = projectilePool.GetComponent<ProjectileObject>();
                projectileObject.InitializeProjectile(shootTarget,trajectoryMaxSpeed,trajectoryMaxHeight,currentPower,host);
                projectileObject.InitializeAnimationCurve(projectileCurve,axisCorrectionProjectileCurve,speedProjectileCurve);
            }
            else
            {
                GameObject projectilePool = ProjectilePooling.instance.GetPoopsProjectile();
                if (projectilePool != null) {
                    projectilePool.transform.position = shootPoint.transform.position;
                    projectilePool.transform.rotation = shootPoint.transform.rotation;
                    projectilePool.SetActive(true);
                }
                ProjectileObject projectileObject = projectilePool.GetComponent<ProjectileObject>();
                projectileObject.InitializeProjectile(shootTarget,trajectoryMaxSpeed,trajectoryMaxHeight,currentPower,host);
                projectileObject.InitializeAnimationCurve(projectileCurve,axisCorrectionProjectileCurve,speedProjectileCurve);
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
        GetComponent<PlayerController>().TakeHeal();
        GetComponent<IUnit>().EndTurn();
        healButton.SetActive(false);
    }
}
