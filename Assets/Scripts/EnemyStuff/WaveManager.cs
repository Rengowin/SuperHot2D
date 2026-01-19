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

        StartCoroutine(RunWaves());
    }

    IEnumerator RunWaves()
    {
        while (currentWaveIndex < waves.Count)
        {
            yield return new WaitForSeconds(timeBetweenWaves);

            onWaveStart?.Invoke();

            Wave wave = waves[currentWaveIndex];
            aliveThisWave = wave.enemyCount;
            spawnFinished = false;

            // Tell spawn manager to spawn this wave (count + delay)
            spawnManager.RunWave(
                wave.enemyCount,
                wave.spawnDelay,
                OnEnemyDied,
                () => spawnFinished = true
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
}
