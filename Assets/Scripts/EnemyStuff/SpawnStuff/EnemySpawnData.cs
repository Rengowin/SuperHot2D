using UnityEngine;

[System.Serializable]
public class EnemySpawnData
{
    [Header("Max in this Level")]
    [SerializeField] private int maxEnemiesInLevel;
    [SerializeField] private int countHowManySpawnedInLevel;

    [Header("Prefab")]
    [SerializeField] private GameObject enemyPrefab;

    [SerializeField] private EnemySpawnInfos enemyStats;

    public int MaxEnemiesInLevel => maxEnemiesInLevel;
    public int CountHowManySpawnedInLevel
    {
        get => countHowManySpawnedInLevel;
        set => countHowManySpawnedInLevel = value;
    }

    public GameObject EnemyPrefab => enemyPrefab;
    public EnemySpawnInfos EnemyStats => enemyStats;
}
