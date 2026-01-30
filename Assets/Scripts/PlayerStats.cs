using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

public class PlayerStats : MonoBehaviour
{
    [Header("HP")]
    [SerializeField] private float maxHP = 100f;
    [SerializeField] private float currentHP = 100f;

    [Header("Events")]
    public UnityEvent onDamaged;
    public UnityEvent onDeath;

    public event Action<float, float> onHealthChanged;
    [SerializeField] private Animator animator;

    [Header("Lose Condition")]
    [SerializeField] private string gameOverSceneName = "";

    private bool isDying = false;

    public float MaxHP => maxHP;
    public float HP => currentHP;
    public bool IsDead => currentHP <= 0f;

    void Awake()
    {
        if (!animator)
        animator = GetComponentInChildren<Animator>();
        currentHP = Mathf.Clamp(currentHP, 0f, maxHP);
        onHealthChanged?.Invoke(currentHP, maxHP);
    }

    public void TakeDamage(float amount)
{
    if (isDying || amount <= 0f) return;

    currentHP = Mathf.Clamp(currentHP - amount, 0f, maxHP);

    onDamaged?.Invoke();
    onHealthChanged?.Invoke(currentHP, maxHP);

    if (currentHP <= 0f && !isDying)
    {
        isDying = true;
        StartCoroutine(DeathRoutine());
    }
}

    public void Heal(float amount)
    {
        if (isDying || amount <= 0f) return;

        currentHP = Mathf.Clamp(currentHP + amount, 0f, maxHP);
        onHealthChanged?.Invoke(currentHP, maxHP);
    }

private IEnumerator DeathRoutine()
{
    onDeath?.Invoke();

    var movement = GetComponent<Movement>();
if (movement != null)
    movement.enabled = false;

var col = GetComponent<Collider>();
if (col != null)
    col.enabled = false;


    UnityEngine.Rigidbody rb = GetComponent<UnityEngine.Rigidbody>();
    if (rb != null)
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;
    }

    animator?.SetBool("Die", true);

    yield return new WaitForSecondsRealtime(1f);

    if (!string.IsNullOrEmpty(gameOverSceneName))
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(gameOverSceneName);
    }
    else
    {
        Time.timeScale = 0f;
    }
}
}
