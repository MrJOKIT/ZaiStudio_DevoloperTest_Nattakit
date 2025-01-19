using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyProjectileThrower : MonoBehaviour
{
    [Header("Projectile Settings")]
    public GameObject projectile;
    public Transform shootPoint;
    public Transform shootTarget;
    private Vector3 startTarget;
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
    

    private void Awake()
    {
        startTarget = shootTarget.position;
    }
    
    public void ResetTarget()
    {
        shootTarget.position = startTarget;
    }
    public void EnemyFireProjectile(IUnit host,bool success)
    {
        if (success)
        {
            shootTarget.position = new Vector3(Random.Range(startPerfectAttackPoint.x,endPerfectAttackPoint.x), shootTarget.position.y,0);
        }
        else
        {
            shootTarget.position = new Vector3(Random.Range(startAttackPoint.x,endAttackPoint.x), shootTarget.position.y,0);
        }
        
        var projectileObject = Instantiate(projectile, shootPoint.position, shootPoint.rotation).GetComponent<ProjectileObject>();
        projectileObject.InitializeProjectile(shootTarget,trajectoryMaxSpeed,trajectoryMaxHeight,currentPower,host);
        projectileObject.InitializeAnimationCurve(projectileCurve,axisCorrectionProjectileCurve,speedProjectileCurve);
        
        GameManager.instance.GetComponent<TurnManager>().StopTimer();
    }
}
