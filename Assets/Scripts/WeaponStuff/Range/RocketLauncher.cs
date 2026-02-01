using UnityEngine;
using static Bullet;
public class RocketLauncher : Range
{
    public WeaponsEnum WeaponType => WeaponsEnum.RocketLauncher;
    float explosionRadius;

    public RocketLauncher(float damage, float range, float attackCooldown, float maxAmmo, float blastRadius, float bulletSpeed)
    {
        this.damage = damage;
        this.range = range;
        this.attackCooldown = attackCooldown;
        this.maxAmmo = maxAmmo;
        this.explosionRadius = blastRadius;
        this.bulletSpeed = bulletSpeed;
        Init();
    }

    public RocketLauncher(WeaponStats stats)
    {
        this.damage = stats.Damage;
        this.range = stats.Range;
        this.attackCooldown = stats.Cooldown;
        this.maxAmmo = stats.Ammo;
        this.explosionRadius = stats.ExplosionRadius;
        this.bulletSpeed = stats.BulletSpeed;
        Init();
    }

    public float BlastRadius => explosionRadius;
    public float ExplosionRadius
    {
        get => explosionRadius;
        set => explosionRadius = value;
    }

    public override void Shoot(Vector3 aimDir)
    {
        var init = new BulletInit
        {
            damage = damage,
            speed = bulletSpeed,
            maxDistance = range,
            explosive = true,
            explosionRadius = explosionRadius,
            explosionDamage = damage / 2
        };

        SpawnBullet(aimDir, projectilePrefab, init);
    }

    public override bool TryApplyUpgrade(WeaponUpgradeType type, ModiferType modiferType, float value)
    {
        if (base.TryApplyUpgrade(type, modiferType, value))
        {
            return true;
        }

        if (type == WeaponUpgradeType.ExplosionRadius)
        {
            if (modiferType == ModiferType.Add)
            {
                ExplosionRadius += value;
            }
            else if (modiferType == ModiferType.Multiply)
            {
                ExplosionRadius *= value;
            }
            return true;
        }

        return false;
    }

}