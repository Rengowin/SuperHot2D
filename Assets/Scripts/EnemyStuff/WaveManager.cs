using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaveManager : MonoBehaviour
{
    [Header("Waves")]
    public List<Wave> waves = new List<Wave>();
    public Transform[] spawnPoints;

    [Header("Timing")]
    public float timeBetweenWaves = 2f;

    [Header("Events")]
    public UnityEvent onWaveStart;
    public UnityEvent onWaveComplete;
    public UnityEvent onAllWavesComplete;

    private int currentWaveIndex = 0;
    private int enemiesAlive = 0;
    private bool isSpawning = false;

    void Start()
    {
        StartCoroutine(StartNextWave());
    }

    IEnumerator StartNextWave()
    {
        if (currentWaveIndex >= waves.Count)
        {
            onAllWavesComplete?.Invoke();
            yield break;
        }

        yield return new WaitForSeconds(timeBetweenWaves);

        onWaveStart?.Invoke();
        isSpawning = true;

        Wave wave = waves[currentWaveIndex];

        for (int i = 0; i < wave.enemyCount; i++)
        {
            SpawnEnemy(wave.enemyPrefab);
            yield return new WaitForSeconds(wave.spawnDelay);
        }

        isSpawning = false;
    }

    void SpawnEnemy(GameObject prefab)
    {
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject enemy = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);

        enemiesAlive++;

        Enemy enemyScript = enemy.GetComponent<Enemy>();
        if (enemyScript != null)
        {
            enemyScript.onDeath += OnEnemyKilled;
        }
    }

    void OnEnemyKilled()
    {
        enemiesAlive--;

        if (enemiesAlive <= 0 && !isSpawning)
        {
            currentWaveIndex++;
            onWaveComplete?.Invoke();
            StartCoroutine(StartNextWave());
        }
    }
}
