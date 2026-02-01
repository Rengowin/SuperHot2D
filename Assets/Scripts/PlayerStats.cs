using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    [Header("HP")]
    [SerializeField] private float maxHP = 100f;
    [SerializeField] private float currentHP = 100f;

    [Header("Events")]
    public UnityEvent onDamaged;
    public UnityEvent onDeath;

    [Header("Lose Condition")]
    [Tooltip("If set, load this scene when player dies (e.g. MainMenu). Leave empty to just freeze + log.")]
    [SerializeField] private string gameOverSceneName = "";

    public float MaxHP {
            get { return maxHP; } 
            set {maxHP = value; }
    }
    public float HP {
            get { return currentHP; }
            set {if (value <= maxHP)
                    currentHP = value;
                 else
                    currentHP = maxHP;
            }
    }
    public bool IsDead => currentHP <= 0f;

    void Awake()
    {
        currentHP = Mathf.Clamp(currentHP, 0f, maxHP);
    }

    public void TakeDamage(float amount)
    {
        if (IsDead) return;
        if (amount <= 0f) return;

        currentHP = Mathf.Clamp(currentHP - amount, 0f, maxHP);
        onDamaged?.Invoke();

        if (currentHP <= 0f)
            Die();
    }

    public void Heal(float amount)
    {
        if (IsDead) return;
        if (amount <= 0f) return;

        currentHP = Mathf.Clamp(currentHP + amount, 0f, maxHP);
    }

    private void Die()
    {
        onDeath?.Invoke();
        Debug.Log("PLAYER DIED - Game Over");

        // Simple lose condition:
        if (!string.IsNullOrEmpty(gameOverSceneName))
        {
            SceneManager.LoadScene(gameOverSceneName);
            return;
        }

        // Fallback: freeze the game
        Time.timeScale = 0f;
    }
    

}