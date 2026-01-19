using UnityEngine;

public class ProjectileTimeTestMover : TimeAffectable
{
    [Header("Back & Forth Movement")]
    public Vector3 direction = Vector3.forward;
    public float speed = 10f;
    public float distance = 4f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
        direction.Normalize();
    }

    void Update()
    {
        
        float offset = Mathf.Sin(Time.time * speed) * distance;

        transform.position = startPos + direction * offset * LocalTimeScale;
    }
}
