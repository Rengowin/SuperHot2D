using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class EnemySpawnManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    List<EnemySpawnData> enemySpawnDataList;
    [SerializeField]
    List<GameObject> spawnPoints;

    [SerializeField]
    float spawnInterval;
    float spawnTimer;
    [SerializeField]
    int maxEnemies;

    int currentEnemyCount;
    int spawnPointIndex;

    [SerializeField]
    GameObject enemyContainer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentEnemyCount < maxEnemies)
        {
            spawnTimer += Time.deltaTime;
            if(spawnTimer >= spawnInterval)
            {
                spawnTimer = 0f;
                SpawnEnemy();
            }
        }
    }

    private void SpawnEnemy()
    {
        int randomEnemyIndex = Random.Range(0, enemySpawnDataList.Count);
        EnemySpawnData enemySpawnData = enemySpawnDataList[randomEnemyIndex];
        if (enemySpawnData.CountHowManySpawnedInLevel < enemySpawnData.MaxEnemiesInLevel)
        {
            GameObject newEnemy = Instantiate(enemySpawnData.EnemyPrefab, spawnPoints[spawnPointIndex].transform.position, Quaternion.identity);
            newEnemy.transform.parent = enemyContainer.transform;
            enemySpawnData.CountHowManySpawnedInLevel++;
            currentEnemyCount++;

            //here we need also the init for the enmeys

            spawnPointIndex++;
            if(spawnPointIndex >= spawnPoints.Count)
            {
                spawnPointIndex = 0;
            }
        }
    }
}

