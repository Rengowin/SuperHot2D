using UnityEngine;

public class EnemyMover : TimeAffectable
{
    [Header("Target")]
    [SerializeField] private Transform player;

    [Header("Movement")]
    [SerializeField] private float minDistanceToPlayer = 1.5f;

    private Enemy enemy;

    void Awake()
    {
        enemy = GetComponent<Enemy>();

        // Auto-find player if not assigned
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) player = p.transform;
        }
    }

    void Update()
    {
        if (player == null || enemy == null)
            return;

        Vector3 currentPosition = transform.position;
        Vector3 targetPosition = player.position;

        float distance = Vector3.Distance(currentPosition, targetPosition);
        if (distance <= minDistanceToPlayer)
            return;

        Vector3 direction = (targetPosition - currentPosition).normalized;

        // Use the Enemy's MovementSpeed stat
        float speed = enemy.MovementSpeed;

        transform.position += direction * speed * ScaledDeltaTime;
    }
}
