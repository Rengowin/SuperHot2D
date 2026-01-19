using UnityEngine;

public class Swords : Melee
{
    float swingAngle;
    public Swords(float damage, float range, float attackCooldown, float swingAngle)
    {
        this.damage = damage;
        this.range = range;
        this.attackCooldown = attackCooldown;
        this.swingAngle = swingAngle;
        Init();
    }

    public float SwingAngle => swingAngle;


    public override void Swing()
    {
        Debug.Log("Swinging the sword!");
    }
}