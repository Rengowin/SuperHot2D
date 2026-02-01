using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] private List<EnemySpawnData> enemySpawnDataList;
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private Transform enemyContainer;

    [SerializeField] private int maxEnemiesAlive = 10;

    private int currentAlive;
    private int spawnPointIndex;

    private Action onEnemyDied;
    private Action onWaveSpawnFinished;

    // ✅ NEW: Run a wave with specific spawn entries
    public void RunWave(Wave wave, Action enemyDiedCallback, Action waveSpawnFinishedCallback)
    {
        onEnemyDied = enemyDiedCallback;
        onWaveSpawnFinished = waveSpawnFinishedCallback;

        StopAllCoroutines();
        StartCoroutine(SpawnWaveRoutine(wave));
    }

    private IEnumerator SpawnWaveRoutine(Wave wave)
    {
        if (wave == null || wave.spawns == null || wave.spawns.Count == 0)
        {
            onWaveSpawnFinished?.Invoke();
            yield break;
        }

        foreach (var entry in wave.spawns)
        {
            int spawnedOfType = 0;

            while (spawnedOfType < entry.count)
            {
                if (currentAlive < maxEnemiesAlive)
                {
                    if (TrySpawnOne(entry.enemyTypeIndex, entry.useBossPrefab))
                        spawnedOfType++;
                }

                yield return new WaitForSeconds(wave.spawnDelay);
            }
        }

        onWaveSpawnFinished?.Invoke();
    }

    private bool TrySpawnOne(int enemyTypeIndex, bool useBossPrefab)
{
    if (enemySpawnDataList == null || enemySpawnDataList.Count == 0) return false;
    if (spawnPoints == null || spawnPoints.Count == 0) return false;

    enemyTypeIndex = Mathf.Clamp(enemyTypeIndex, 0, enemySpawnDataList.Count - 1);

    var data = enemySpawnDataList[enemyTypeIndex];
    if (data == null || data.EnemyStats == null) return false;

    // per-level cap (can block later waves!)
    if (data.CountHowManySpawnedInLevel >= data.MaxEnemiesInLevel)
        return false;

    Transform sp = spawnPoints[spawnPointIndex];
    spawnPointIndex = (spawnPointIndex + 1) % spawnPoints.Count;

    GameObject prefabToSpawn = data.EnemyPrefab;

    if (useBossPrefab && data.BossEnemyPrefab != null)
        prefabToSpawn = data.BossEnemyPrefab;

    if (prefabToSpawn == null) return false;

    GameObject enemyObj = Instantiate(prefabToSpawn, sp.position, sp.rotation);
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

}
