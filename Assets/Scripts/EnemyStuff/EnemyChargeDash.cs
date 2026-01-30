using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyChargeDash : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform player;

    [Header("Trigger")]
    [SerializeField] private float triggerDistance = 5f;

    [Header("Charge")]
    [SerializeField] private float chargeTime = 1f;

    [Header("Dash")]
    [SerializeField] private float dashSpeed = 12f;
    [SerializeField] private float stopDistance = 0.2f;

    [Header("Cooldown")]
    [SerializeField] private float dashCooldown = 2f;

    [Header("Ground Lock")]
    [SerializeField] private bool lockY = true;     // ✅ keeps enemy from falling
    [SerializeField] private float groundY = 0f;    // ✅ where enemy should stay

    private Rigidbody rb;
    private bool isCharging;
    private bool isDashing;
    private float lastDashTime = -999f;
    private Vector3 dashTarget;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        // Optional: strongest fix if you're fully top-down
        // rb.useGravity = false;
        // rb.constraints |= RigidbodyConstraints.FreezePositionY;
    }

    void Update()
    {
        if (player == null) return;
        if (isCharging || isDashing) return;
        if (Time.time < lastDashTime + dashCooldown) return;

        float dist = Vector3.Distance(Flat(transform.position), Flat(player.position));
        if (dist <= triggerDistance)
            StartCoroutine(ChargeAndDash());
    }

    void LateUpdate()
    {
        // ✅ Hard lock Y so it never drifts down
        if (lockY)
        {
            Vector3 p = transform.position;
            p.y = groundY;
            transform.position = p;

            Vector3 v = rb.linearVelocity;
            rb.linearVelocity = new Vector3(v.x, 0f, v.z);
        }
    }

    private IEnumerator ChargeAndDash()
    {
        isCharging = true;

        // stop moving while charging
        rb.linearVelocity = Vector3.zero;

        yield return new WaitForSeconds(chargeTime);

        dashTarget = Flat(player.position);

        isCharging = false;
        isDashing = true;
        lastDashTime = Time.time;

        while (isDashing)
        {
            Vector3 current = Flat(transform.position);
            Vector3 toTarget = dashTarget - current;

            if (toTarget.magnitude <= stopDistance)
                break;

            Vector3 dir = toTarget.normalized;
            Vector3 vel = dir * dashSpeed;

            rb.linearVelocity = new Vector3(vel.x, 0f, vel.z);

            yield return null;
        }

        rb.linearVelocity = Vector3.zero;
        isDashing = false;
    }

    private Vector3 Flat(Vector3 v) => new Vector3(v.x, 0f, v.z);

    public void SetPlayer(Transform t) => player = t;
}
