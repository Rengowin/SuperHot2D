using UnityEngine;
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

    public override void Shoot()
    {
        Debug.Log("Shooting the rocket!");
        Debug.Log($"Blast radius: {blastRadius}");
    }
}
