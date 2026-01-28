using UnityEngine;

public abstract class Melee : Weapon
{
    Transform meleeOrigin;

    public Transform MeleeOrigin
    {
        get => meleeOrigin;
        set => meleeOrigin = value;
    }
    public override bool canAttack()
    {
        return Time.time >= lastAttackTime + attackCooldown;
    }

    public override void attack()
    {
        attack(origin.forward);
    }

    public void attack(Vector3 aimDir)
    {
        if (!canAttack()) return;
        Swing(aimDir);
        lastAttackTime = Time.time;
    }

    public abstract void Swing(Vector3 aimDir);


}