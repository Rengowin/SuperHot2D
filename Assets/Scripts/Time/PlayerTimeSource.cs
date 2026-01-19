using UnityEngine;

public class PlayerTimeSource : MonoBehaviour
{
    public static PlayerTimeSource Instance { get; private set; }

    [Tooltip("Velocity considered as full-speed time")]
    public float maxPlayerSpeed = 10f;

    private Rigidbody rb;

    public float NormalizedSpeed { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        float speed = rb.linearVelocity.magnitude;
        NormalizedSpeed = Mathf.Clamp01(speed / maxPlayerSpeed);
    }
}
