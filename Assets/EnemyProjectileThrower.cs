using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public enum EnemyAttackType
{
    Normal,
    Perfect,
    Double,
}
public class EnemyProjectileThrower : MonoBehaviour
{
    [Header("Projectile Settings")]
    public GameObject projectile;
    public GameObject projectilePower;
    public Transform shootPoint;
    public Transform shootTarget;
    [Header("Trajectory Settings")] 
    public float trajectoryMaxSpeed = 10f;
    public float trajectoryMaxHeight = 0.5f;
    public PowerList currentPower = PowerList.None;
    public AnimationCurve projectileCurve, axisCorrectionProjectileCurve, speedProjectileCurve;
    [Space(10)] 
    [Header("Attack Settings")]
    [SerializeField] private Vector2 startAttackPoint;
    [SerializeField] private Vector2 endAttackPoint;
    [Space(10)] 
    [SerializeField] private Vector2 startPerfectAttackPoint;
    [SerializeField] private Vector2 endPerfectAttackPoint;
    public bool isUsePowerAttack;
    public bool isUseDoubleAttack;
    
    
    
    public void EnemyFireProjectile(IUnit host,EnemyAttackType enemyAttackType)
    {
        if (enemyAttackType == EnemyAttackType.Perfect)
        {
            shootTarget.position = new Vector3(Random.Range(startPerfectAttackPoint.x,endPerfectAttackPoint.x), shootTarget.position.y,0);
        }
        else if (enemyAttackType == EnemyAttackType.Normal)
        {
            shootTarget.position = new Vector3(Random.Range(startAttackPoint.x,endAttackPoint.x), shootTarget.position.y,0);
        }
        else if (enemyAttackType == EnemyAttackType.Double)
        {
            shootTarget.position = new Vector3(shootTarget.position.x, shootTarget.position.y,0);
        }

        if (GameManager.instance.enemyDifficulty == EnemyDifficulty.Normal && GameManager.instance.CurrentWindForce < 0.5f && isUseDoubleAttack == false)
        {
            // double attack
            currentPower = PowerList.DoubleAttack;
            var projectileObject = Instantiate(projectile, shootPoint.position, shootPoint.rotation).GetComponent<ProjectileObject>();
            projectileObject.InitializeProjectile(shootTarget,trajectoryMaxSpeed,trajectoryMaxHeight,currentPower,host);
            projectileObject.InitializeAnimationCurve(projectileCurve,axisCorrectionProjectileCurve,speedProjectileCurve);
            isUseDoubleAttack = true;
            GetComponent<EnemyController>().isDoubleAttackUse = true;
        }
        else if (GameManager.instance.enemyDifficulty == EnemyDifficulty.Hard && GameManager.instance.CurrentWindForce > 2f && isUsePowerAttack == false)
        {
            currentPower = PowerList.PowerThrow;
            var projectileObject = Instantiate(projectilePower, shootPoint.position, shootPoint.rotation).GetComponent<ProjectileObject>();
            projectileObject.InitializeProjectile(shootTarget,trajectoryMaxSpeed,trajectoryMaxHeight,currentPower,host);
            projectileObject.InitializeAnimationCurve(projectileCurve,axisCorrectionProjectileCurve,speedProjectileCurve);
            isUsePowerAttack = true;
        }
        else
        {
            var projectileObject = Instantiate(projectile, shootPoint.position, shootPoint.rotation).GetComponent<ProjectileObject>();
            projectileObject.InitializeProjectile(shootTarget,trajectoryMaxSpeed,trajectoryMaxHeight,currentPower,host);
            projectileObject.InitializeAnimationCurve(projectileCurve,axisCorrectionProjectileCurve,speedProjectileCurve);
        }
        
        GameManager.instance.GetComponent<TurnManager>().StopTimer();
    }
}
