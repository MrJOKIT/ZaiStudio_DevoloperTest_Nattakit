using System;
using System.Collections.Generic;
using NorskaLib.Spreadsheets;
using UnityEngine;

namespace _James.Script.GoogleSheet
{
    [CreateAssetMenu(fileName = "DataBase", menuName = "Data/DataBase")]
    public class DataBase : SpreadsheetsContainerBase
    {
        [SpreadsheetContent]
        [SerializeField] private DataBaseContent content;
        public DataBaseContent Content => content;
    }

    [Serializable]
    public class DataBaseContent
    {
        [SpreadsheetPage("PlayerData")]
        public List<PlayerData> PlayerData;
        [SpreadsheetPage("EnemyData")]
        public List<EnemyData> EnemyData;
        [SpreadsheetPage("DamageData")]
        public List<DamageData> DamageData;
        [SpreadsheetPage("SettingData")]
        public List<SettingData> SettingData;
    }

    [Serializable]
    public class PlayerData
    {
        public string Name;
        public float HP;
    }

    [Serializable]
    public class EnemyData
    {
        public EnemyDifficulty Name;
        public float HP;
        public float MissedChance;
        public PowerList PowerList;
    }

    [Serializable]
    public class DamageData
    {
        public AttackType Name;
        public int amount;
        public float Damage;
    }

    [Serializable]
    public class SettingData
    {
        public string Name;
        public float Sec;
    }
}