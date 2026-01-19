using UnityEngine;

public class EnemyDeathListener : MonoBehaviour
{
    private WaveManager waveManager;

    public void Init(WaveManager manager)
    {
        waveManager = manager;
    }

    void OnDestroy()
    {
        if (waveManager != null)
        {
            waveManager.OnEnemyKilled();
        }
    }
}