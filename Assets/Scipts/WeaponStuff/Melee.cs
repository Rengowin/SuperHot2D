using UnityEngine;

public abstract class Melee : Weapon
{
    public override bool canAttack()
    {
        return Time.time >= lastAttackTime + attackCooldown;
    }

    public override void attack()
    {
        if (canAttack())
        {
            Swing();
            lastAttackTime = Time.time;
            Debug.Log(damage);
        }
        else
        {
            Debug.Log("Weapon is on cooldown!");
        }
    }

    public abstract void Swing();
}
