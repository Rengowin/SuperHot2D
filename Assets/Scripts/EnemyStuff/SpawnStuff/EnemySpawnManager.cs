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

    private bool isBossRound;

    private int currentAlive;
    private int spawnPointIndex;

    // Callbacks to talk back to WaveManager
    private Action onEnemyDied;
    private Action onWaveSpawnFinished;

    // Run one wave: spawn enemyCount enemies, waiting spawnDelay between spawns
    public void RunWave(int enemyCount, float spawnDelay, bool bossRound, Action enemyDiedCallback, Action waveSpawnFinishedCallback)
    {
        onEnemyDied = enemyDiedCallback;
        onWaveSpawnFinished = waveSpawnFinishedCallback;
        isBossRound = bossRound;
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

    // ---------- BOSS ROUND ----------
    if (isBossRound)
    {
        // Find a data entry that has a boss prefab
        EnemySpawnData bossData = null;
        for (int i = 0; i < enemySpawnDataList.Count; i++)
        {
            if (enemySpawnDataList[i] != null && enemySpawnDataList[i].BossEnemyPrefab != null)
            {
                bossData = enemySpawnDataList[i];
                break;
            }
        }

        if (bossData == null)
        {
            Debug.LogError("Boss round, but no BossEnemyPrefab assigned in any EnemySpawnData!");
            return false;
        }

        Transform sp = spawnPoints[spawnPointIndex];
        spawnPointIndex = (spawnPointIndex + 1) % spawnPoints.Count;

        GameObject enemyObj = Instantiate(bossData.BossEnemyPrefab, sp.position, sp.rotation);
        if (enemyContainer != null) enemyObj.transform.SetParent(enemyContainer, true);

        Enemy enemy = enemyObj.GetComponent<Enemy>();
        if (enemy != null && bossData.BossStats != null)
        {
            enemy.init(
                bossData.EnemyStats.HP,
                bossData.EnemyStats.MovementSpeed,
                (int)bossData.EnemyStats.Damage,
                bossData.EnemyStats.Weapon
            );
        }

        currentAlive++;

        var listener = enemyObj.AddComponent<EnemyDeathListener>();
        listener.Init(() =>
        {
            currentAlive = Mathf.Max(0, currentAlive - 1);
            onEnemyDied?.Invoke();
        });

        return true;
    }

    // ---------- NORMAL ROUND ----------
    for (int tries = 0; tries < 10; tries++)
    {
        var data = enemySpawnDataList[UnityEngine.Random.Range(0, enemySpawnDataList.Count)];
        if (data == null || data.EnemyPrefab == null || data.EnemyStats == null) continue;
        if (data.CountHowManySpawnedInLevel >= data.MaxEnemiesInLevel) continue;

        Transform sp = spawnPoints[spawnPointIndex];
        spawnPointIndex = (spawnPointIndex + 1) % spawnPoints.Count;

        GameObject enemyObj = Instantiate(data.EnemyPrefab, sp.position, sp.rotation);
        if (enemyContainer != null) enemyObj.transform.SetParent(enemyContainer, true);

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
