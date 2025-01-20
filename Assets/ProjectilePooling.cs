using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePooling : MonoBehaviour
{
    public static ProjectilePooling instance;
    [Header("Bone Projectile")]
    public int bonePoolSize;
    public GameObject boneProjectile;
    public List<GameObject> bonePool;
    [Space(10)] 
    public int bonePowerPoolSize;
    public GameObject bonePowerProjectile;
    public List<GameObject> bonePowerPool;
    [Space(20)] 
    [Header("Poops Projectile")]
    public int poopsPoolSize;
    public GameObject poopsProjectile;
    public List<GameObject> poopsPool;
    [Space(10)] 
    public int poopsPowerPoolSize;
    public GameObject poopsPowerProjectile;
    public List<GameObject> poopsPowerPool;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        SetPool(bonePool,boneProjectile,bonePoolSize);
        SetPool(bonePowerPool,bonePowerProjectile,bonePowerPoolSize);
        SetPool(poopsPool,poopsProjectile,poopsPoolSize);
        SetPool(poopsPowerPool,poopsPowerProjectile,poopsPowerPoolSize);
    }

    private void SetPool(List<GameObject> poolList,GameObject poolObject,int poolSize)
    {
        GameObject tmp;
        for (int i = 0; i < poolSize; i++)
        {
            tmp = Instantiate(poolObject);
            tmp.SetActive(false);
            poolList.Add(tmp);
        }
    }

    public GameObject GetBoneProjectile()
    {
        for(int i = 0; i < bonePoolSize; i++)
        {
            if(!bonePool[i].activeInHierarchy)
            {
                return bonePool[i];
            }
        }
        return null;
    }

    public GameObject GetBonePowerProjectile()
    {
        for(int i = 0; i < bonePowerPoolSize; i++)
        {
            if(!bonePowerPool[i].activeInHierarchy)
            {
                return bonePowerPool[i];
            }
        }
        return null;
    }
    
    public GameObject GetPoopsProjectile()
    {
        for(int i = 0; i < poopsPoolSize; i++)
        {
            if(!poopsPool[i].activeInHierarchy)
            {
                return poopsPool[i];
            }
        }
        return null;
    }
    
    public GameObject GetPoopsPowerProjectile()
    {
        for(int i = 0; i < poopsPowerPoolSize; i++)
        {
            if(!poopsPowerPool[i].activeInHierarchy)
            {
                return poopsPowerPool[i];
            }
        }
        return null;
    }
    
}
