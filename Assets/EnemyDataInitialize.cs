using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _James.Script.GoogleSheet;
using UnityEngine;

[RequireComponent(typeof(EnemyController))]
public class EnemyDataInitialize : MonoBehaviour
{
    [SerializeField] private DataBase dataBase;
    private void Awake()
    {
        var data = dataBase.Content.EnemyData.First(x => x.Name == GameManager.instance.enemyDifficulty);
        GetComponent<EnemyController>().InitializedEnemy(data.HP,data.MissedChance,data.PowerList);
    }
}
