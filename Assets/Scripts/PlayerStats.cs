using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System;

public class PlayerStats : MonoBehaviour
{
    [Header("HP")]
    [SerializeField] private float maxHP = 100f;
    [SerializeField] private float currentHP = 100f;

    [Header("Events")]
    public UnityEvent onDamaged;
    public UnityEvent onDeath;

    // 🔹 New: health update event (current, max)
    public event Action<float, float> onHealthChanged;

    [Header("Lose Condition")]
    [Tooltip("If set, load this scene when player dies (e.g. MainMenu). Leave empty to just freeze + log.")]
    [SerializeField] private string gameOverSceneName = "";

    public float MaxHP => maxHP;
    public float HP => currentHP;
    public bool IsDead => currentHP <= 0f;

    void Awake()
    {
        currentHP = Mathf.Clamp(currentHP, 0f, maxHP);

        // 🔹 Notify UI on start
        onHealthChanged?.Invoke(currentHP, maxHP);
    }

    public void TakeDamage(float amount)
    {
        if (IsDead) return;
        if (amount <= 0f) return;

        currentHP = Mathf.Clamp(currentHP - amount, 0f, maxHP);

        onDamaged?.Invoke();
        onHealthChanged?.Invoke(currentHP, maxHP);

        if (currentHP <= 0f)
            Die();
    }

    public void Heal(float amount)
    {
        if (IsDead) return;
        if (amount <= 0f) return;

        currentHP = Mathf.Clamp(currentHP + amount, 0f, maxHP);
        onHealthChanged?.Invoke(currentHP, maxHP);
    }

    private void Die()
    {
        onDeath?.Invoke();
        Debug.Log("PLAYER DIED - Game Over");

        if (!string.IsNullOrEmpty(gameOverSceneName))
        {
            SceneManager.LoadScene(gameOverSceneName);
            return;
        }

        Time.timeScale = 0f;
    }
}