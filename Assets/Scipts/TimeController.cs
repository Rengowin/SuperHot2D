using UnityEngine;

public class TimeController : MonoBehaviour
{
    [Header("Time Scale")]
    [Range(0f, 1f)]
    public float stoppedTimeScale = 0.02f;

    [Range(0f, 1f)]
    public float maxTimeScale = 1f;

    [Header("Response Speeds")]
    public float timeSpeedUp = 4f;    // when moving
    public float timeSlowDown = 12f;  // when stopping 

    [Header("Movement Detection")]
    public float minSpeed = 0.05f;

    private Rigidbody rb;
    private float targetTimeScale;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Time.timeScale = stoppedTimeScale;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }

    private void Update()
    {
        bool moving = IsPlayerMoving();

        targetTimeScale = moving ? maxTimeScale : stoppedTimeScale;

        float responseSpeed = moving ? timeSpeedUp : timeSlowDown;

        Time.timeScale = Mathf.Lerp(
            Time.timeScale,
            targetTimeScale,
            responseSpeed * Time.unscaledDeltaTime
        );

        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }

    private bool IsPlayerMoving()
    {
        return rb != null && rb.linearVelocity.magnitude > minSpeed;
    }

    private void OnDestroy() //make sure the world unfreeze after destroy
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
    }
}
