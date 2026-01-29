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

    public Swords(WeaponStats stats)
    {
        this.damage = stats.Damage;
        this.range = stats.Range;
        this.attackCooldown = stats.Cooldown;
        this.swingAngle = stats.SwingAngle;
        Init();
    }
    public float SwingAngle => swingAngle;


    public override void Swing(Vector3 aimDir)
    {
        Vector3 center = origin.position;
        float radius = range;
        float halfAngle = swingAngle * 0.5f;

        // Topdown: auf XZ flach halten
        aimDir.y = 0f;
        if (aimDir.sqrMagnitude < 0.0001f) aimDir = origin.forward;
        aimDir.Normalize();

        Collider[] hits = Physics.OverlapSphere(center, radius);
        var damaged = new System.Collections.Generic.HashSet<Enemy>();

        foreach (var h in hits)
        {
            Enemy enemy = h.GetComponentInParent<Enemy>();
            if (enemy == null || !damaged.Add(enemy)) continue;

            Vector3 closest = h.ClosestPoint(center);
            Vector3 toEnemy = closest - center;
            toEnemy.y = 0f;

            if (toEnemy.sqrMagnitude < 0.0001f)
            {
                enemy.HP -= damage;
                continue;
            }

            float angle = Vector3.Angle(aimDir, toEnemy);
            if (angle <= halfAngle)
                enemy.HP -= damage;
        }
    }

}