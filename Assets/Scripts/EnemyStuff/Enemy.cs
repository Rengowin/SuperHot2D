using UnityEngine;
using System;
using System.Collections;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] float hp;
    [SerializeField] float movementSpeed;
    [SerializeField] float damage;

    [Header("Death")]
    [SerializeField] private float deathDelay = 1.2f;

    public event Action<Enemy> onDeath;

    private bool isDying;

    private Animator animator;
    private Rigidbody rb;
    private Collider col;
    private AudioSource audioSource;

    [Header("Audio")]
    [SerializeField] private AudioClip deathSound;

    public float HP
    {
        get => hp;
        set
        {
            if (isDying) return;

            hp = value;
            Debug.Log($"Enemy HP set to: {hp}");

            if (hp <= 0f)
                StartCoroutine(DeathRoutine());
        }
    }

    public float MovementSpeed
    {
        get => movementSpeed;
        set => movementSpeed = value;
    }

    public float Damage
    {
        get => damage;
        set => damage = value;
    }

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0f;
    }

    public void init(float hp, float movementSpeed, int damage, Weapon weapon)
    {
        this.hp = hp;
        MovementSpeed = movementSpeed;
        Damage = damage;
    }

    private IEnumerator DeathRoutine()
    {
        if (isDying) yield break;
        isDying = true;

        Debug.Log("Enemy died");
        onDeath?.Invoke(this);
        if (rb)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
        }

        if (col)
            col.enabled = false;

        if (animator)
            animator.SetBool("Die", true);

        if (deathSound)
            audioSource.PlayOneShot(deathSound);

        yield return new WaitForSeconds(deathDelay);

        Destroy(gameObject);
    }
}
