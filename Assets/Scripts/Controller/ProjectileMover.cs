using UnityEngine;

public class ProjectileMover : TimeAffectable
{
    public Vector3 velocity;

    void Update()
    {
        transform.position += velocity * ScaledDeltaTime;
    }
}
