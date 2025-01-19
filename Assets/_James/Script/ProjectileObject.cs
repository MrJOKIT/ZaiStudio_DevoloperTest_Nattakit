using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileObject : MonoBehaviour
{
    [Header("Projectile Target")] 
    private Transform target;
    private float moveSpeed;
    private float maxMoveSpeed;
    private float trajectoryMaxRelativeHeight;
    private Vector3 trajectoryStartPoint;
    
    [Header("References")]
    private IUnit host;
    private bool isPowerThrow;
    private bool isDoubleAttack;
    private AnimationCurve projectileCurve;
    private AnimationCurve axisCorrectionProjectileCurve;
    private AnimationCurve speedProjectileCurve;
    //private Rigidbody2D rb;
    private void Start()
    {
        trajectoryStartPoint = transform.position;
    }
    
    private void Update()
    {
        UpdateProjectileCurve();
        
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
        
        /*float angle = Mathf.Atan2(rb.velocity.x, rb.velocity.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);*/

        /*if (Vector3.Distance(transform.position, target.position) < 1f)
        {
            Destroy(gameObject);
        }*/ //ให้ไปโดน Collision แล้วทำลายตัวเองดีกว่า
    }

    private void UpdateProjectileCurve()
    {
        Vector3 trajectoryRange = target.position - trajectoryStartPoint;

        if (trajectoryRange.x < 0)
        {
            moveSpeed = -moveSpeed;
        }
        
        float nextPositionX = transform.position.x + moveSpeed  * Time.deltaTime;
        float nextPositionXNormalized = (nextPositionX - trajectoryStartPoint.x) / trajectoryRange.x;
        
        float nextPositionYNormalized = projectileCurve.Evaluate(nextPositionXNormalized);
        
        float nextPositionYCorrectionNormalize = axisCorrectionProjectileCurve.Evaluate(nextPositionXNormalized);
        float nextPositionYCorrectionAbsolute = nextPositionYCorrectionNormalize * trajectoryRange.y;
        
        float nextPositionY = trajectoryStartPoint.y + nextPositionYNormalized * trajectoryMaxRelativeHeight + nextPositionYCorrectionAbsolute;
        
        Vector3 newPosition = new Vector3(nextPositionX, nextPositionY, 0);
        
        CalculateNextProjectileSpeed(nextPositionXNormalized);
        
        transform.position = newPosition;
    }

    private void CalculateNextProjectileSpeed(float nextPositionXNormalized) 
    {
        float nextMoveSpeedNormalized = speedProjectileCurve.Evaluate(nextPositionXNormalized);
        
        moveSpeed = nextMoveSpeedNormalized * maxMoveSpeed;
    }

    public void InitializeProjectile(Transform target, float maxMoveSpeed,float trajectoryMaxHeight,PowerList powerList,IUnit host)
    {
        this.host = host;
        this.target = target;
        this.maxMoveSpeed = maxMoveSpeed;
        float xDistanceToTarget = target.position.x - transform.position.x;
        this.trajectoryMaxRelativeHeight = Mathf.Abs(xDistanceToTarget * trajectoryMaxHeight);
        switch (powerList)
        {
            case PowerList.PowerThrow:
                isPowerThrow = true;
                break;
            case PowerList.DoubleAttack:
                isDoubleAttack = true;
                break;
        }
    }

    public void InitializeAnimationCurve(AnimationCurve animationCurve, AnimationCurve axisCorrectionAnimationCurve,AnimationCurve speedAnimationCurve)
    {
        this.projectileCurve = animationCurve;
        this.axisCorrectionProjectileCurve = axisCorrectionAnimationCurve;
        this.speedProjectileCurve = speedAnimationCurve;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Head"))
        {
            if (isPowerThrow)
            {
                if (other.gameObject.GetComponent<TakeDamagePoint>() != null)
                {
                    other.gameObject.GetComponent<TakeDamagePoint>().player.TakeDamage(GameManager.instance.GetComponent<WorldDamage>().GetPowerDamage());
                }
                else if (other.gameObject.GetComponent<EnemyTakeDamagePoint>() != null)
                {
                    other.gameObject.GetComponent<EnemyTakeDamagePoint>().enemy.TakeDamage(GameManager.instance.GetComponent<WorldDamage>().GetPowerDamage());
                }
            }
            else if (isDoubleAttack)
            {
                if (other.gameObject.GetComponent<TakeDamagePoint>() != null)
                {
                    other.gameObject.GetComponent<TakeDamagePoint>().player.TakeDamage(GameManager.instance.GetComponent<WorldDamage>().GetDoubleDamage());
                }
                else if (other.gameObject.GetComponent<EnemyTakeDamagePoint>() != null)
                {
                    other.gameObject.GetComponent<EnemyTakeDamagePoint>().enemy.TakeDamage(GameManager.instance.GetComponent<WorldDamage>().GetDoubleDamage());
                }
            }
            else
            { 
                if (other.gameObject.GetComponent<TakeDamagePoint>() != null)
                {
                    other.gameObject.GetComponent<TakeDamagePoint>().player.TakeDamage(GameManager.instance.GetComponent<WorldDamage>().GetNormalDamage());
                }
                else if (other.gameObject.GetComponent<EnemyTakeDamagePoint>() != null)
                {
                    other.gameObject.GetComponent<EnemyTakeDamagePoint>().enemy.TakeDamage(GameManager.instance.GetComponent<WorldDamage>().GetNormalDamage());
                }
            }
            Debug.Log("Head");
            host.EndTurn();
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Body"))
        {
            if (isPowerThrow)
            {
                if (other.gameObject.GetComponent<TakeDamagePoint>() != null)
                {
                    other.gameObject.GetComponent<TakeDamagePoint>().player.TakeDamage(GameManager.instance.GetComponent<WorldDamage>().GetPowerDamage());
                }
                else if (other.gameObject.GetComponent<EnemyTakeDamagePoint>() != null)
                {
                    other.gameObject.GetComponent<EnemyTakeDamagePoint>().enemy.TakeDamage(GameManager.instance.GetComponent<WorldDamage>().GetPowerDamage());
                }
            }
            else if (isDoubleAttack)
            {
                if (other.gameObject.GetComponent<TakeDamagePoint>() != null)
                {
                    other.gameObject.GetComponent<TakeDamagePoint>().player.TakeDamage(GameManager.instance.GetComponent<WorldDamage>().GetDoubleDamage());
                }
                else if (other.gameObject.GetComponent<EnemyTakeDamagePoint>() != null)
                {
                    other.gameObject.GetComponent<EnemyTakeDamagePoint>().enemy.TakeDamage(GameManager.instance.GetComponent<WorldDamage>().GetDoubleDamage());
                }
            }
            else
            {
                if (other.gameObject.GetComponent<TakeDamagePoint>() != null)
                {
                    other.gameObject.GetComponent<TakeDamagePoint>().player.TakeDamage(GameManager.instance.GetComponent<WorldDamage>().GetSmallDamage());
                }
                else if (other.gameObject.GetComponent<EnemyTakeDamagePoint>() != null)
                {
                    other.gameObject.GetComponent<EnemyTakeDamagePoint>().enemy.TakeDamage(GameManager.instance.GetComponent<WorldDamage>().GetSmallDamage());
                }
            } 
            Debug.Log("Body"); 
            host.EndTurn();
            Destroy(gameObject);
        }
        else if (other.gameObject.CompareTag("Ground"))
        {
            host.EndTurn();
            Destroy(gameObject);
        }
    }
}
