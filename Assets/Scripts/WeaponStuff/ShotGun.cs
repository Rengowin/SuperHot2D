using UnityEngine;
using static Bullet;
public class ShotGun : Range
{
    float bulletcount;
    float spreadAngle;
    public ShotGun(float bulletcount, float spreadAngle, float damage, float range, float attackCooldown, float maxAmmo)
    {
        this.bulletcount = bulletcount;
        this.spreadAngle = spreadAngle;
        this.damage = damage;
        this.range = range;
        this.attackCooldown = attackCooldown;
        this.maxAmmo = maxAmmo;
        Init();
    }

    public int Pellets => Mathf.Max(1, Mathf.RoundToInt(bulletcount));
    public float SpreadAngle => spreadAngle;

    public override void Shoot(Vector3 aimDir)
    {
        var init = new BulletInit
        {
            damage = damage,
            speed = 50f,
            maxDistance = range,
            explosive = false
        };

        SpawnBullet(aimDir, projectilePrefab, init);
    }


}