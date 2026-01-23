using UnityEngine;
using static Bullet;
public class RocketLauncher : Range
{
    float blastRadius;

    public RocketLauncher(float damage, float range, float attackCooldown, float maxAmmo, float blastRadius)
    {
        this.damage = damage;
        this.range = range;
        this.attackCooldown = attackCooldown;
        this.maxAmmo = maxAmmo;
        this.blastRadius = blastRadius;
        Init();
    }

    public float BlastRadius => blastRadius;

    public override void Shoot(Vector3 aimDir)
    {
        var init = new BulletInit
        {
            damage = damage,
            speed = 25f,
            maxDistance = range,
            explosive = true,
            explosionRadius = blastRadius,
            explosionDamage = damage
        };

        SpawnBullet(aimDir, projectilePrefab, init);
    }

}