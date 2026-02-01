using UnityEngine;
using System;

public class EnemyDeathListener : MonoBehaviour
{
    private Action onDestroyed;
    private bool fired;

    public void Init(Action onDestroyedCallback)
    {
        onDestroyed = onDestroyedCallback;
    }

    void OnDestroy()
    {
        if (fired) return;
        fired = true;
        onDestroyed?.Invoke();
    }
}
