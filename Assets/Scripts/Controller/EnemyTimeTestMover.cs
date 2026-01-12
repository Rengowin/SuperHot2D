using UnityEngine;

public class EnemyTimeTestMover : TimeAffectable
{
    [Header("Enemy Movement")]
    public float speed = 2f;
    public float distance = 3f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float offset = Mathf.Sin(Time.time * speed) * distance;
        transform.position = startPos + Vector3.right * offset * LocalTimeScale;
    }
}
