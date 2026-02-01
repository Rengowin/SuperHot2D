using System;                    // ✅ REQUIRED
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaveManager : MonoBehaviour
{
    [Header("Waves")]
    public List<Wave> waves = new List<Wave>();

    public event Action<int, int, bool> onWaveChanged;
    public int CurrentWaveNumber => currentWaveIndex + 1;
    public int TotalWaves => waves != null ? waves.Count : 0;

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

            aliveThisWave = CountEnemiesInWave(wave);
            spawnFinished = false;

            onWaveChanged?.Invoke(CurrentWaveNumber, TotalWaves, wave.bossRound);
            
            spawnManager.RunWave(
            wave,
            OnEnemyDied,
            OnWaveSpawnFinished
            );

            // Wait until spawner finished spawning AND all enemies died
            while (!spawnFinished || aliveThisWave > 0)
                yield return null;
            
            onWaveChanged?.Invoke(CurrentWaveNumber, TotalWaves, false);
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
    private int CountEnemiesInWave(Wave wave)
{
    int total = 0;
    if (wave?.spawns == null) return 0;
    foreach (var s in wave.spawns) total += Mathf.Max(0, s.count);
    return total;
}
}