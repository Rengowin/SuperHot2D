using UnityEngine;
using static Bullet;

public class AllRangeTogether:Range
{
    
    float pelletCount;
    float spreadAngle;

    float explosionRadius;

    public int Pellets => Mathf.Max(1, Mathf.RoundToInt(pelletCount));
    public float SpreadAngle => spreadAngle;
    public float BlastRadius => explosionRadius;

    public AllRangeTogether(float bulletcount, float spreadAngle, float damage, float range, float attackCooldown, float maxAmmo, float bulletSpeed, float blastRadius)
    {
        this.pelletCount = bulletcount;
        this.spreadAngle = spreadAngle;
        this.damage = damage;
        this.range = range;
        this.attackCooldown = attackCooldown;
        this.maxAmmo = maxAmmo;
        this.bulletSpeed = bulletSpeed;
        this.explosionRadius = blastRadius;
        Init();
    }

    public AllRangeTogether(WeaponStats stats)
    {
        this.pelletCount = stats.PelletCount;
        this.spreadAngle = stats.Spread;
        this.damage = stats.Damage;
        this.range = stats.Range;
        this.attackCooldown = stats.Cooldown;
        this.maxAmmo = stats.Ammo;
        this.bulletSpeed = stats.BulletSpeed;
        this.explosionRadius = stats.ExplosionRadius;
        Init();
    }

    Vector3 ApplySpread(Vector3 dir, float spreadAngleDeg)
    {
        float yaw = Random.Range(-spreadAngleDeg, spreadAngleDeg);
        float pitch = Random.Range(-spreadAngleDeg, spreadAngleDeg);
        return (Quaternion.Euler(pitch, yaw, 0f) * dir).normalized;
    }

    public override void Shoot(Vector3 aimDir)
    {
        for (int i = 0; i < pelletCount; i++)
        {
            Vector3 dir = ApplySpread(aimDir, spreadAngle);
            var init = new BulletInit
            {
                damage = damage,   // oder damage
                speed = bulletSpeed,
                maxDistance = range,
                explosive = true,
                explosionRadius = explosionRadius,
                explosionDamage = damage / 2
            };
            SpawnBullet(dir, projectilePrefab, init);
        }
    }
}
