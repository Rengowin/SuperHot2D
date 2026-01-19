using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] private List<EnemySpawnData> enemySpawnDataList;
    [SerializeField] private List<Transform> spawnPoints;

    [SerializeField] private Transform enemyContainer;

    // global alive cap (same as your maxEnemies)
    [SerializeField] private int maxEnemiesAlive = 10;

    private int currentAlive;
    private int spawnPointIndex;

    // Callbacks to talk back to WaveManager
    private Action onEnemyDied;
    private Action onWaveSpawnFinished;

    // Run one wave: spawn enemyCount enemies, waiting spawnDelay between spawns
    public void RunWave(int enemyCount, float spawnDelay, Action enemyDiedCallback, Action waveSpawnFinishedCallback)
    {
        onEnemyDied = enemyDiedCallback;
        onWaveSpawnFinished = waveSpawnFinishedCallback;

        StopAllCoroutines();
        StartCoroutine(SpawnWaveRoutine(enemyCount, spawnDelay));
    }

    private IEnumerator SpawnWaveRoutine(int enemyCount, float spawnDelay)
    {
        int spawned = 0;

        while (spawned < enemyCount)
        {
            // wait until we have room under alive cap
            if (currentAlive < maxEnemiesAlive)
            {
                if (TrySpawnOne())
                {
                    spawned++;
                }
            }

            yield return new WaitForSeconds(spawnDelay);
        }

        onWaveSpawnFinished?.Invoke();
    }

    private bool TrySpawnOne()
    {
        if (enemySpawnDataList == null || enemySpawnDataList.Count == 0) return false;
        if (spawnPoints == null || spawnPoints.Count == 0) return false;

        // Try a few times to find a type that isn't capped
        for (int tries = 0; tries < 10; tries++)
        {
            var data = enemySpawnDataList[UnityEngine.Random.Range(0, enemySpawnDataList.Count)];
            if (data == null || data.EnemyPrefab == null || data.EnemyStats == null) continue;
            if (data.CountHowManySpawnedInLevel >= data.MaxEnemiesInLevel) continue;

            Transform sp = spawnPoints[spawnPointIndex];
            spawnPointIndex = (spawnPointIndex + 1) % spawnPoints.Count;

            GameObject enemyObj = Instantiate(data.EnemyPrefab, sp.position, sp.rotation);
            if (enemyContainer != null) enemyObj.transform.SetParent(enemyContainer, true);

            // Apply stats using YOUR Enemy API (lowercase init)
            Enemy enemy = enemyObj.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.init(
                    data.EnemyStats.HP,
                    data.EnemyStats.MovementSpeed,
                    (int)data.EnemyStats.Damage,
                    data.EnemyStats.Weapon
                );
            }

            data.CountHowManySpawnedInLevel++;
            currentAlive++;

            // death callback using your Action-based listener
            var listener = enemyObj.AddComponent<EnemyDeathListener>();
            listener.Init(() =>
            {
                currentAlive = Mathf.Max(0, currentAlive - 1);
                onEnemyDied?.Invoke();
            });

            return true;
        }

        return false;
    }
}
