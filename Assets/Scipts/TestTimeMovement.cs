using UnityEngine;

public class TestTimeMovement : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 2f;
    public float distance = 3f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // This movement is affected by Time.timeScale
        float offset = Mathf.Sin(Time.time * speed) * distance;

        transform.position = startPos + Vector3.right * offset;
    }
}
