using UnityEngine;
public class ShotGun : Range
{
    float bulletcount;

    public ShotGun(float bulletcount, float damage, float range, float attackCooldown, float maxAmmo)
    {
        this.bulletcount = bulletcount;
        this.damage = damage;
        this.range = range;
        this.attackCooldown = attackCooldown;
        this.maxAmmo = maxAmmo;
        Init();
    }

    public override void Shoot()
    {
        Debug.Log("Shooting the shotgun!");
        Debug.Log($"Bullets fired: {bulletcount}");
    }


}