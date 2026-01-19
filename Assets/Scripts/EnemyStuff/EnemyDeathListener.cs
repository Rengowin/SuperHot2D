using UnityEngine;
using System;

public class EnemyDeathListener : MonoBehaviour
{
    private Action onDestroyed;

    public void Init(Action onDestroyedCallback)
    {
        onDestroyed = onDestroyedCallback;
    }

    void OnDestroy()
    {
        onDestroyed?.Invoke();
    }
}
