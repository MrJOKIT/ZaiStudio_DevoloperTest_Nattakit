using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Awake()
    {
        startTarget = shootTarget.position;
    }

    public void ResetTarget()
    {
        shootTarget.position = startTarget;
    }
    public void FireProjectile(IUnit host)
    {
        var projectileObject = Instantiate(projectile, shootPoint.position, shootPoint.rotation).GetComponent<ProjectileObject>();
        projectileObject.InitializeProjectile(shootTarget,trajectoryMaxSpeed,trajectoryMaxHeight,currentPower,host);
        projectileObject.InitializeAnimationCurve(projectileCurve,axisCorrectionProjectileCurve,speedProjectileCurve);
        
        GameManager.instance.GetComponent<TurnManager>().StopTimer();
    }
}
