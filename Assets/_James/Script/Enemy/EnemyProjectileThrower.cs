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
    public Vector2 doubleAttackPoint;
    public bool doubleAttacking;
    
    
    
    public void EnemyFireProjectile(IUnit host,EnemyAttackType enemyAttackType)
    {
        if (SoundManager.instance != null)
        {
            SoundManager.instance.PlaySound(SoundName.EnemyThrow);
        }

        if (doubleAttacking)
        {
            shootTarget.position = doubleAttackPoint;
            doubleAttacking = false;
        }
        else
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
                doubleAttackPoint = new Vector3(shootTarget.position.x, shootTarget.position.y,0);
                shootTarget.position = doubleAttackPoint;
                doubleAttacking = true;
            }
        }
        

        if (GetComponent<EnemyController>().enemyPower == PowerList.DoubleAttack && GameManager.instance.CurrentWindForce < 0.5f && GetComponent<EnemyController>().isUsePower == false)
        {
            // double attack
            currentPower = PowerList.DoubleAttack;
            GetComponent<EnemyController>().isUsePower = true;
            GetComponent<EnemyController>().isDoubleAttackUse = true;
            GameObject projectilePool = ProjectilePooling.instance.GetPoopsProjectile();
            if (projectilePool != null) {
                projectilePool.transform.position = shootPoint.transform.position;
                projectilePool.transform.rotation = shootPoint.transform.rotation;
                projectilePool.SetActive(true);
            }
            var projectileObject = projectilePool.GetComponent<ProjectileObject>();
            projectileObject.InitializeProjectile(shootTarget,trajectoryMaxSpeed,trajectoryMaxHeight,currentPower,host);
            projectileObject.InitializeAnimationCurve(projectileCurve,axisCorrectionProjectileCurve,speedProjectileCurve);
        }
        if (GetComponent<EnemyController>().enemyPower == PowerList.PowerThrow && GameManager.instance.CurrentWindForce > 2f && GetComponent<EnemyController>().isUsePower == false)
        {
            currentPower = PowerList.PowerThrow;
            GameObject projectilePool = ProjectilePooling.instance.GetPoopsPowerProjectile();
            if (projectilePool != null) {
                projectilePool.transform.position = shootPoint.transform.position;
                projectilePool.transform.rotation = shootPoint.transform.rotation;
                projectilePool.SetActive(true);
            }
            var projectileObject = projectilePool.GetComponent<ProjectileObject>();
            projectileObject.InitializeProjectile(shootTarget,trajectoryMaxSpeed,trajectoryMaxHeight,currentPower,host);
            projectileObject.InitializeAnimationCurve(projectileCurve,axisCorrectionProjectileCurve,speedProjectileCurve);
            GetComponent<EnemyController>().isUsePower = true;
        }
        else
        {
            GameObject projectilePool = ProjectilePooling.instance.GetPoopsProjectile();
            if (projectilePool != null) {
                projectilePool.transform.position = shootPoint.transform.position;
                projectilePool.transform.rotation = shootPoint.transform.rotation;
                projectilePool.SetActive(true);
            }
            var projectileObject = projectilePool.GetComponent<ProjectileObject>();
            projectileObject.InitializeProjectile(shootTarget,trajectoryMaxSpeed,trajectoryMaxHeight,currentPower,host);
            projectileObject.InitializeAnimationCurve(projectileCurve,axisCorrectionProjectileCurve,speedProjectileCurve);
        }
        
        GameManager.instance.GetComponent<TurnManager>().StopTimer();
    }
}
