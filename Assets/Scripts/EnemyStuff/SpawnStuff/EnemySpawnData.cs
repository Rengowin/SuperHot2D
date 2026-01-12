using NUnit.Framework;
using UnityEngine;

[System.Serializable]
public class EnemySpawnData
{
    [Header("Max in this Level")]
    [SerializeField]
    int maxEnemiesInLevel;
    //probley worng since if we reuse the same level/Secne we need to change it per code
    [SerializeField]
    int countHowManySpawnedInLevel;

    [Header("Prefab")]
    [SerializeField]
    GameObject enemyPrefab;

    //[Header("Enemy Stats")]
    //doesnt needed because the header from enemySpawnInfos will also been shown
    [SerializeField]
    EnemySpawnInfos enemyStats;


    //getters and setters
    public int MaxEnemiesInLevel { get { return maxEnemiesInLevel; } }
    public int CountHowManySpawnedInLevel { get { return countHowManySpawnedInLevel; } set { countHowManySpawnedInLevel = value; } }

    public GameObject EnemyPrefab { get { return enemyPrefab; } }

    public EnemySpawnInfos EnemyStats { get { return enemyStats; } }
}
