using UnityEngine;

[RequireComponent(typeof(PlayerStats))]
public class DamageReceiver : MonoBehaviour
{
    [Header("Hit Cooldown")]
    [SerializeField] private float contactHitCooldown = 0.25f;

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

        var touch = other.GetComponentInParent<DamageOnTouch>();
        if (touch != null)
        {
            stats.TakeDamage(touch.ContactDamage);
            lastContactHitTime = Time.time;
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
