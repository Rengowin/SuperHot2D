using UnityEngine;

public class Swords : Melee
{

    public Swords(float damage, float range, float attackCooldown) 
    {
        this.damage = damage;
        this.range = range;
        this.attackCooldown = attackCooldown;
        Init();
    }
    public override void Swing()
    {
        Debug.Log("Swinging the sword!");
    }
}
