using UnityEngine;

public class EnemyMover : TimeAffectable
{
    [Header("Target")]
    [SerializeField] private Transform player;

    [Header("Movement")]
    [SerializeField] private float speed = 2f;
    [SerializeField] private float minDistanceToPlayer = 1.5f;

    void Update()
    {
        if (player == null)
            return;

        Vector3 currentPosition = transform.position;
        Vector3 targetPosition = player.position;

        float distance = Vector3.Distance(currentPosition, targetPosition);

        if (distance <= minDistanceToPlayer)
            return;

        Vector3 direction = (targetPosition - currentPosition).normalized;

        transform.position += direction * speed * ScaledDeltaTime;
    }
}
