using UnityEngine;

[RequireComponent(typeof(PlayerStats))]
public class DamageReceiver : MonoBehaviour
{
    [Header("Hit Cooldown")]
    [SerializeField] private float contactHitCooldown = 0.25f;
    [SerializeField] private Animator animator;

    private PlayerStats stats;
    private float lastContactHitTime = -999f;

    void Awake()
    {
        stats = GetComponent<PlayerStats>();
    }

    void OnCollisionEnter(Collision collision)
    {
        TryTakeContactDamage(collision.collider);
        TryTakeBulletDamage(collision.collider);
    }

    void OnTriggerEnter(Collider other)
    {
        TryTakeContactDamage(other);
        TryTakeBulletDamage(other);
    }

    private void TryTakeContactDamage(Collider other)
{
    if (Time.time < lastContactHitTime + contactHitCooldown)
        return;

    DamageOnTouch damageTouch = other.GetComponentInParent<DamageOnTouch>();
    if (damageTouch != null)
    {
        stats.TakeDamage(damageTouch.ContactDamage);
        lastContactHitTime = Time.time;
        return;
    }

    ExplodeOnTouch explodeTouch = other.GetComponentInParent<ExplodeOnTouch>();
    if (explodeTouch != null)
    {
        stats.TakeDamage(explodeTouch.ContactDamage);
        lastContactHitTime = Time.time;
        Destroy(other.transform.root.gameObject);
        return;
    }
}


    private void TryTakeBulletDamage(Collider other)
    {
        var bullet = other.GetComponentInParent<Bullet>();
        if (bullet == null) return;

        if (bullet.owner != null && bullet.owner == gameObject)
            return;

       
        if (bullet.owner != null && bullet.owner == transform.root.gameObject)
            return;

        stats.TakeDamage(bullet.damage);
}
}
