using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _James.Script.GoogleSheet;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerDataInitialize : MonoBehaviour
{
    [SerializeField] private DataBase dataBase;
    [SerializeField] private string playerName = "Player";
    [SerializeField] private string healName = "Heal";

    private void Awake()
    {
        var data = dataBase.Content.PlayerData.First(x=> x.Name == playerName);
        var heal = dataBase.Content.PlayerData.First(x=> x.Name == healName);
        GetComponent<PlayerController>().InitializePlayer(data.HP,heal.HP);
    }
}
