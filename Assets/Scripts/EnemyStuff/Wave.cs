using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Wave
{
    [Header("Spawns in this wave")]
    public List<WaveSpawnEntry> spawns = new List<WaveSpawnEntry>();

    [Header("Timing")]
    public float spawnDelay = 0.5f;

    [Header("Flags")]
    public bool bossRound;
}

[Serializable]
public class WaveSpawnEntry
{
    [Tooltip("Index into EnemySpawnManager.enemySpawnDataList")]
    public int enemyTypeIndex = 0;

    [Tooltip("How many of this enemy to spawn")]
    public int count = 5;

    [Tooltip("If true, use BossEnemyPrefab (if set) for this entry")]
    public bool useBossPrefab = false;
}
