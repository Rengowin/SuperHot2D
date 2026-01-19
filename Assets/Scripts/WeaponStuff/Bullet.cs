using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Lifetime")]
    [SerializeField] private float lifeTime = 2f;

    [Header("Explosion (optional)")]
    public bool isExplosive = false;
    public float explosionRadius = 3f;
    public float explosionDamage = 20f;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Bullet hit: " + collision.collider.name);

        if (isExplosive)
        {
            Explode();
        }

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
