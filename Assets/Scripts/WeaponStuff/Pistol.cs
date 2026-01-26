using UnityEngine;
using static Bullet;
public class Pistol : Range
{

    public Pistol(float damage, float ranage, float attackCooldown, float maxAmmo)
    {
        this.damage = damage;
        this.range = ranage;
        this.attackCooldown = attackCooldown;
        this.maxAmmo = maxAmmo;
        Init();
    }
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