using UnityEngine;
using static Bullet;
public class Pistol : Range
{

    public Pistol(float damage, float range, float attackCooldown, float maxAmmo, float bulletSpeed)
    {
        this.damage = damage;
        this.range = range;
        this.attackCooldown = attackCooldown;
        this.maxAmmo = maxAmmo;
        this.bulletSpeed = bulletSpeed;
        Init();
    }

    public Pistol(WeaponStats stats)
    {
        this.damage = stats.Damage;
        this.range = stats.Range;
        this.attackCooldown = stats.Cooldown;
        this.maxAmmo = stats.Ammo;
        this.bulletSpeed = stats.BulletSpeed;
        Init();
    }

    public override void Shoot(Vector3 aimDir)
    {
        var init = new BulletInit
        {
            damage = damage,
            speed = bulletSpeed,
            maxDistance = range,
            explosive = false
        };
        SpawnBullet(aimDir, projectilePrefab, init);
    }


}