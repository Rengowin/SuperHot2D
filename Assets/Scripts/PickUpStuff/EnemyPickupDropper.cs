using UnityEngine;

public class EnemyPickupDropper : MonoBehaviour
{
    [Range(0f, 1f)]
    [SerializeField] private float dropChance = 0.10f;

    [Tooltip("Pickups prefabs to spawn (Heal, Ammo, etc). One will be chosen randomly.")]
    [SerializeField] private GameObject[] pickupPrefabs;

    [Tooltip("Optional: small random offset so it doesn't spawn inside enemy collider.")]
    [SerializeField] private float spawnRadius = 0.5f;

    public void TryDrop()
    {
        if (pickupPrefabs == null || pickupPrefabs.Length == 0) return;
        if (Random.value > dropChance) return;

        Vector3 offset = new Vector3(
            Random.Range(-spawnRadius, spawnRadius),
            0f,
            Random.Range(-spawnRadius, spawnRadius)
        );

        GameObject prefab = pickupPrefabs[Random.Range(0, pickupPrefabs.Length)];
        Instantiate(prefab, transform.position + offset, Quaternion.identity);
    }
}
