using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectileThrow : MonoBehaviour
{
    [Header("References")]
    public Transform shootPoint;
    public GameObject projectile;
    [Space(20)]
    [Header("Projectile Setting")]
    [SerializeField] private float startProjectileSpeed = 15;
    [SerializeField] private float endProjectileSpeed = 50;
    public float GetEndProjectileSpeed { get { return endProjectileSpeed; } }

    [Space(20)] 
    [Header("Current Speed")] 
    public Image shootBar;
    public float projectileSpeed;

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
        var projectileObject = Instantiate(projectile,shootPoint.position,shootPoint.rotation);
       // float currentForce = GameManager.instance.WindForce + projectileSpeed;
        Vector2 velocity = shootPoint.up * projectileSpeed; //currentForce แทน projectileSpeed
        projectileObject.GetComponent<ProjectileObject>().host = host;
        projectileObject.GetComponent<Rigidbody2D>().velocity = velocity;
        
        projectileSpeed = startProjectileSpeed;
    }
}
