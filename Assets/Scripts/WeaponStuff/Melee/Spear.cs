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

    public Spear(WeaponStats stats)
    {
        this.damage = stats.Damage;
        this.range = stats.Range;
        this.attackCooldown = stats.Cooldown;
        this.thrustRadius = stats.ThrustRadius;
        Init();
    }

    public float ThrustRadius => thrustRadius;
    public override void Swing(Vector3 aimDir)
    {
        Vector3 start = origin.position;
        float distance = range;

        aimDir.y = 0f;
        if (aimDir.sqrMagnitude < 0.0001f) aimDir = origin.forward;
        aimDir.Normalize();

        // kleiner Offset, damit du nicht "im eigenen Collider" startest
        start += aimDir * 0.05f;

        Ray ray = new Ray(start, aimDir);
        RaycastHit[] hits = Physics.SphereCastAll(ray, thrustRadius, distance);

        System.Array.Sort(hits, (a, b) => a.distance.CompareTo(b.distance));

        var damaged = new System.Collections.Generic.HashSet<Enemy>();
        foreach (var hit in hits)
        {
            Enemy enemy = hit.collider.GetComponentInParent<Enemy>();
            if (enemy == null || !damaged.Add(enemy)) continue;

            enemy.HP -= damage;
        }
    }

}