using UnityEngine;

public abstract class Range : Weapon
{
    [SerializeField]
    protected float ammo;

    [SerializeField]
    protected float maxAmmo;

    public float MaxAmmo => maxAmmo;
    public float Ammo => ammo;

    public override void Init()
    {
        base.Init();
        ammo = maxAmmo;
    }

    public override bool canAttack()
    {
        if (ammo > 0)
        {
            return Time.time >= lastAttackTime + attackCooldown;
        }
        else
        {
            Debug.Log("Out of ammo!");
            return false;
        }
    }

    public override void attack()
    {
        if (canAttack())
        {
            Shoot();
            lastAttackTime = Time.time;
            ammo--;
            Debug.Log(damage);
        }
        else
        {
            Debug.Log("Weapon is on cooldown or out of ammo!");
        }
    }

    public virtual void Reload()
    {
        ammo = maxAmmo;
        Debug.Log("Weapon reloaded!");
    }

    public abstract void Shoot();
}
