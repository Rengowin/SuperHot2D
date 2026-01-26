using UnityEngine;

public class EnemyVisualController : MonoBehaviour
{
    [Header("Render")]
    [SerializeField] private SpriteRenderer sprite;

    [Header("Animators")]
    [SerializeField] private Animator animator;
    [SerializeField] private RuntimeAnimatorController frontController;
    [SerializeField] private RuntimeAnimatorController backController;

    [Header("Target")]
    [SerializeField] private Transform player;

    [Header("Facing")]
    [SerializeField] private float deadZone = 0.05f;

    private bool usingBack = false;

    void Awake()
    {
        if (sprite == null)
            sprite = GetComponentInChildren<SpriteRenderer>(true);

        if (animator == null)
            animator = GetComponentInChildren<Animator>(true);

        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null) player = p.transform;
        }

        // Ensure animator starts with a valid controller
        if (animator != null && animator.runtimeAnimatorController == null && frontController != null)
        {
            animator.runtimeAnimatorController = frontController;
        }
    }

    void LateUpdate()
    {
        if (sprite == null || animator == null || player == null)
            return;

        Vector3 p = player.position;
        Vector3 me = transform.position;

        // ---------- FLIP LEFT / RIGHT (X axis) ----------
        float dx = p.x - me.x;
        if (Mathf.Abs(dx) > deadZone)
        {
            // Player left -> face left | Player right -> face right
            sprite.flipX = dx < 0f;
        }

        // ---------- FRONT / BACK (Z axis) ----------
        float dz = p.z - me.z;

        if (dz > deadZone && !usingBack)
        {
            usingBack = true;
            if (backController != null)
                animator.runtimeAnimatorController = backController;
        }
        else if (dz < -deadZone && usingBack)
        {
            usingBack = false;
            if (frontController != null)
                animator.runtimeAnimatorController = frontController;
        }
    }
}
