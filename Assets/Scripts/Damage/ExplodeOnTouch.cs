using UnityEngine;

public class ExplodeOnTouch : MonoBehaviour
{
    [Header("Damage")]
    [SerializeField] private float playerDamage = 25f;

    [Header("Optional AOE")]
    [SerializeField] private bool useAOE = false;
    [SerializeField] private float explosionRadius = 3f;
    [SerializeField] private float enemyDamage = 9999f;
    [SerializeField] private LayerMask aoeMask;         

    [Header("One-shot")]
    [SerializeField] private bool destroyThisObjectAfterExplode = true;

    private bool exploded;

    private Enemy enemy;
    private Collider col;

    void Awake()
    {
        enemy = GetComponent<Enemy>();
        col = GetComponent<Collider>();
    }

    void OnCollisionEnter(Collision collision)
    {
        TryExplode(collision.collider);
    }

    void OnTriggerEnter(Collider other)
    {
        TryExplode(other);
    }

    private void TryExplode(Collider other)
    {
        if (exploded) return;
        PlayerStats player = other.GetComponentInParent<PlayerStats>();
        if (player == null) return;

        exploded = true;

        if (col != null) col.enabled = false;

        player.TakeDamage(playerDamage);

        if (useAOE)
            DoAOE();

        if (enemy != null)
        {
            enemy.HP = 0f;
        }
        else
        {
            if (destroyThisObjectAfterExplode)
                Destroy(gameObject);
        }
    }

    private void DoAOE()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius, aoeMask);

        foreach (var h in hits)
        {
            PlayerStats ps = h.GetComponentInParent<PlayerStats>();
            if (ps != null)
            {
                continue;
            }

            
            Enemy e = h.GetComponentInParent<Enemy>();
            if (e != null && e != enemy)
            {
                e.HP -= enemyDamage;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (!useAOE) return;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
