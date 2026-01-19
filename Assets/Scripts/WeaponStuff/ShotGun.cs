using UnityEngine;
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

    public override void Shoot()
    {
        Debug.Log("Shooting the shotgun!");
        Debug.Log($"Bullets fired: {bulletcount}");
    }


}