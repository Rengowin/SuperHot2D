using UnityEngine;

public class Spear : Melee
{
    public Spear(float damage, float range, float attackCooldown) 
    {
        this.damage = damage;
        this.range = range;
        this.attackCooldown = attackCooldown;
        Init();
    }
    public override void Swing()
    {
        Debug.Log("Swinging the Spear!");
    }
}
