using UnityEngine;

public class Spear : Melee
{
    float thrustRadius;
    public Spear(float damage, float range, float attackCooldown, float thrustRadius) 
    {
        this.damage = damage;
        this.range = range;
        this.attackCooldown = attackCooldown;
        this.thrustRadius = thrustRadius;
        Init();
    }

    public float ThrustRadius => thrustRadius;
    public override void Swing()
    {
        Debug.Log("Swinging the Spear!");
    }
}