using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileObject : MonoBehaviour
{
    [Header("References")]
    public IUnit host;
    public bool isPowerThrow;
    public bool isDoubleAttack;
    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    private void Update()
    {
        float angle = Mathf.Atan2(rb.velocity.x, rb.velocity.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Head"))
        {
            if (isPowerThrow)
            {
                if (other.gameObject.GetComponent<TakeDamagePoint>() != null)
                {
                    other.gameObject.GetComponent<TakeDamagePoint>().player.TakeDamage(GameManager.instance.GetComponent<WorldDamage>().GetPowerDamage());
                }
            }
            else if (isDoubleAttack)
            {
                if (other.gameObject.GetComponent<TakeDamagePoint>() != null)
                {
                    other.gameObject.GetComponent<TakeDamagePoint>().player.TakeDamage(GameManager.instance.GetComponent<WorldDamage>().GetDoubleDamage());
                }
            }
            else
            { 
                if (other.gameObject.GetComponent<TakeDamagePoint>() != null)
                {
                    other.gameObject.GetComponent<TakeDamagePoint>().player.TakeDamage(GameManager.instance.GetComponent<WorldDamage>().GetNormalDamage());
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
            }
            else if (isDoubleAttack)
            {
                if (other.gameObject.GetComponent<TakeDamagePoint>() != null)
                {
                    other.gameObject.GetComponent<TakeDamagePoint>().player.TakeDamage(GameManager.instance.GetComponent<WorldDamage>().GetDoubleDamage());
                }
            }
            else
            {
                if (other.gameObject.GetComponent<TakeDamagePoint>() != null)
                {
                    other.gameObject.GetComponent<TakeDamagePoint>().player.TakeDamage(GameManager.instance.GetComponent<WorldDamage>().GetSmallDamage());
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
