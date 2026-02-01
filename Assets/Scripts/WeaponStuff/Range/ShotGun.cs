using UnityEngine;
using static Bullet;
public class ShotGun : Range
{
    public WeaponsEnum WeaponType => WeaponsEnum.ShotGun;
    float pelletCount;
    float spreadAngle;
    public ShotGun(float bulletcount, float spreadAngle, float damage, float range, float attackCooldown, float maxAmmo, float bulletSpeed)
    {
        this.pelletCount = bulletcount;
        this.spreadAngle = spreadAngle;
        this.damage = damage;
        this.range = range;
        this.attackCooldown = attackCooldown;
        this.maxAmmo = maxAmmo;
        this.bulletSpeed = bulletSpeed;
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
        this.bulletSpeed = stats.BulletSpeed;
        Init();
    }

    public float PelletCount
    {
        get => pelletCount;
        set => pelletCount = value;
    }

    public float SpreadAngle
    {
        get => spreadAngle;
        set => spreadAngle = value;
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
                explosive = false
            };

            SpawnBullet(dir, projectilePrefab, init);
        }
    }

    public override bool TryApplyUpgrade(WeaponUpgradeType type, ModiferType modiferType, float value)
    {
        if (base.TryApplyUpgrade(type, modiferType, value))
            return true;

        if (type == WeaponUpgradeType.BulletsPerShot)
        {
            if (modiferType == ModiferType.Add)
            {
                PelletCount += value;
            }
            else if (modiferType == ModiferType.Multiply)
            {
                PelletCount *= value;
            }
            return true;
        }

        if (type == WeaponUpgradeType.BulletSpread)
        {
            if (modiferType == ModiferType.Add)
            {
                SpreadAngle = Mathf.Max(0, SpreadAngle - value);
            }
            else if (modiferType == ModiferType.Multiply)
            {
                SpreadAngle = Mathf.Max(0, SpreadAngle * value);
            }
            return true;
        }

        return false;
    }
}