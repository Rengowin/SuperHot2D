using UnityEngine;
using static Bullet;
public class ShotGun : Range
{
    float pelletCount;
    float spreadAngle;
    public ShotGun(float bulletcount, float spreadAngle, float damage, float range, float attackCooldown, float maxAmmo)
    {
        this.pelletCount = bulletcount;
        this.spreadAngle = spreadAngle;
        this.damage = damage;
        this.range = range;
        this.attackCooldown = attackCooldown;
        this.maxAmmo = maxAmmo;
        Init();
    }

    public ShotGun(WeaponStats stats)
    {
        this.pelletCount = stats.PelletCount;
        this.spreadAngle = stats.Spread;
        this.damage = stats.Damage;
        this.range = stats.Range;
        this.attackCooldown = stats.Cooldown;
        this.maxAmmo = stats.Ammo;
        Init();
    }

    public int Pellets => Mathf.Max(1, Mathf.RoundToInt(pelletCount));
    public float SpreadAngle => spreadAngle;

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
                speed = 50f,
                maxDistance = range,
                explosive = false
            };

            SpawnBullet(dir, projectilePrefab, init);
        }
    }



}