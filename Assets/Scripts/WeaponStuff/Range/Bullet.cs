using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float range;
    public float speed;
    public float damage;
    public bool isExplosive;
    public float explosionRadius;
    public float explosionDamage;

    Vector3 startPos;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPos = transform.position;
    }


    public struct BulletInit
    {
        public float damage;
        public float speed;
        public float maxDistance;

        public bool explosive;
        public float explosionRadius;
        public float explosionDamage;
    }

    public void Init(BulletInit init, Vector3 dir)
    {
        rb = GetComponent<Rigidbody>();
        startPos = transform.position;

        damage = init.damage;
        speed = init.speed;
        range = init.maxDistance;
        isExplosive = init.explosive;
        explosionRadius = init.explosionRadius;
        explosionDamage = init.explosionDamage;

        Launch(dir);
    }


    public void Launch(Vector3 dir)
    {
        if (rb != null) rb.linearVelocity = dir.normalized * speed;
    }

    void Update()
    {
        if (Vector3.Distance(startPos, transform.position) >= range)
            Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        var enemy = collision.collider.GetComponentInParent<Enemy>();
        if (enemy != null && !isExplosive)
            enemy.HP -= damage;

        if (isExplosive) Explode();
        Destroy(gameObject);
    }

    void Explode()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, explosionRadius);

        var damaged = new System.Collections.Generic.HashSet<Enemy>();
        SpawnExplosionLight();
        foreach (var h in hits)
        {
            Enemy enemy = h.GetComponentInParent<Enemy>();
            if (enemy == null || !damaged.Add(enemy)) continue;

            Vector3 closest = h.ClosestPoint(transform.position);
            float distance = Vector3.Distance(transform.position, closest);

            float t = Mathf.Clamp01(distance / explosionRadius);
            float dmg = Mathf.Lerp(explosionDamage, 0f, t);

            enemy.HP -= dmg;

            Debug.Log($"Explosion hit {enemy.name} for {dmg}");
        }

        Debug.Log($"BOOM radius {explosionRadius}");
    }


    private void OnDrawGizmosSelected()
    {
        if (!isExplosive) return;

        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

    void SpawnExplosionLight()
    {
        GameObject lightObj = new GameObject("ExplosionLight");
        lightObj.transform.position = transform.position;

        Light l = lightObj.AddComponent<Light>();
        l.type = LightType.Point;
        l.range = explosionRadius;
        l.intensity = 5f;
        l.color = Color.red;

        Destroy(lightObj, 0.1f);
    }

}
