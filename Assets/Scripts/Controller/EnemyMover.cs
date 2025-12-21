using UnityEngine;

public class EnemyMover : TimeAffectable
{
    public float speed = 2f;
    public Vector3 direction = Vector3.right;

    void Update()
    {
        transform.position += direction * speed * ScaledDeltaTime;
    }
}
