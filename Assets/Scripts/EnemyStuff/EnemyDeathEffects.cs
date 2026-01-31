using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyDeathEffects : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private float volume = 1f;

    private Enemy enemy;
    private AudioSource audioSource;

    void Awake()
    {
        enemy = GetComponent<Enemy>();

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0f;
    }

    void OnEnable()
    {
        enemy.onDeath += PlayDeathEffects;
    }

    void OnDisable()
    {
        enemy.onDeath -= PlayDeathEffects;
    }

    private void PlayDeathEffects(Enemy e)
    {
        if (deathSound)
            audioSource.PlayOneShot(deathSound, volume);
    }
}
