using UnityEngine;
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
    public override void Shoot()
    {
        Debug.Log("Shooting the pistol!");
    }
}
