using UnityEngine;

[System.Serializable]
public class EnemySpawnData
{
    [Header("Max in this Level")]
    [SerializeField] private int maxEnemiesInLevel;
    [SerializeField] private int countHowManySpawnedInLevel;

    [Header("Prefab")]
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject bossEnemyPrefab;
    [SerializeField] private EnemySpawnInfos enemyStats;
    [SerializeField] private EnemySpawnInfos bossStats;
    

    public int MaxEnemiesInLevel => maxEnemiesInLevel;
    public int CountHowManySpawnedInLevel
    {
        get => countHowManySpawnedInLevel;
        set => countHowManySpawnedInLevel = value;
    }

    public GameObject EnemyPrefab => enemyPrefab;
    public EnemySpawnInfos EnemyStats => enemyStats;
    public GameObject BossEnemyPrefab => bossEnemyPrefab;
    public EnemySpawnInfos BossStats => enemyStats;
}
