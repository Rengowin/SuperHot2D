using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerVisualController : MonoBehaviour
{
    [Header("Render")]
    [SerializeField] private SpriteRenderer sprite;

    [Header("Animators")]
    [SerializeField] private Animator animator;
    [SerializeField] private RuntimeAnimatorController frontController;
    [SerializeField] private RuntimeAnimatorController backController;

    [Header("Facing")]
    [SerializeField] private float deadZone = 0.05f;

    private bool usingBack = false;

    void Awake()
    {
        if (sprite == null)
            sprite = GetComponent<SpriteRenderer>();

        if (animator == null)
            animator = GetComponent<Animator>();

        // Ensure Animator always has a controller
        if (animator.runtimeAnimatorController == null && frontController != null)
        {
            animator.runtimeAnimatorController = frontController;
        }
    }

    public void UpdateFacing(Vector3 velocity)
{
    var kb = Keyboard.current;
    if (kb == null || sprite == null || animator == null)
        return;

    // --- FRONT / BACK (W / S ONLY) ---
    bool wPressed = kb.wKey.isPressed;
    bool sPressed = kb.sKey.isPressed;

    if (wPressed && !usingBack)
    {
        usingBack = true;
        if (backController != null)
            animator.runtimeAnimatorController = backController;
    }
    else if (sPressed && usingBack)
    {
        usingBack = false;
        if (frontController != null)
            animator.runtimeAnimatorController = frontController;
    }

    // --- LEFT / RIGHT FLIP (A / D ONLY) ---
    bool aPressed = kb.aKey.isPressed;
    bool dPressed = kb.dKey.isPressed;

    if (aPressed && !dPressed)
    {
        sprite.flipX = true;   // left
    }
    else if (dPressed && !aPressed)
    {
        sprite.flipX = false;  // right
    }
}
}
