using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaveManager : MonoBehaviour
{
    [Header("Waves")]
    public List<Wave> waves = new List<Wave>();

    [Header("Spawner")]
    [SerializeField] private EnemySpawnManager spawnManager;

    [Header("Timing")]
    public float timeBetweenWaves = 2f;

    [Header("Events")]
    public UnityEvent onWaveStart;
    public UnityEvent onWaveComplete;
    public UnityEvent onAllWavesComplete;

    private int currentWaveIndex = 0;

    // Track this wave
    private int aliveThisWave = 0;
    private bool spawnFinished = false;

    void Start()
    {
        if (spawnManager == null)
            spawnManager = FindFirstObjectByType<EnemySpawnManager>();

        if (spawnManager == null)
        {
            Debug.LogError("WaveManager: No EnemySpawnManager found in scene!");
            enabled = false;
            return;
        }

        StartCoroutine(RunWaves());
    }

    IEnumerator RunWaves()
    {
        while (currentWaveIndex < waves.Count)
        {
            yield return new WaitForSeconds(timeBetweenWaves);

            onWaveStart?.Invoke();

            Wave wave = waves[currentWaveIndex];

            // Count how many will be spawned this wave.
            // If you always spawn 1 boss, keep enemyCount = 1 in the wave data.
            aliveThisWave = wave.enemyCount;
            spawnFinished = false;

            spawnManager.RunWave(
                wave.enemyCount,
                wave.spawnDelay,
                wave.bossRound,      // ✅ pass bossRound
                OnEnemyDied,         // ✅ enemy died callback
                OnWaveSpawnFinished  // ✅ spawn finished callback
            );

            // Wait until spawner finished spawning AND all enemies died
            while (!spawnFinished || aliveThisWave > 0)
                yield return null;

            onWaveComplete?.Invoke();
            currentWaveIndex++;
        }

        onAllWavesComplete?.Invoke();
    }

    private void OnEnemyDied()
    {
        aliveThisWave = Mathf.Max(0, aliveThisWave - 1);
    }

    private void OnWaveSpawnFinished()
    {
        spawnFinished = true;
    }
}