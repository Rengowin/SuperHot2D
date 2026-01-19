using UnityEngine;

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

    public void UpdateFacing(Vector3 velocity)
    {
        Vector3 flat = new Vector3(velocity.x, 0f, velocity.z);

        if (flat.magnitude < deadZone)
            return;

        bool shouldUseBack = flat.z > 0f;

        if (shouldUseBack != usingBack)
        {
            usingBack = shouldUseBack;
            animator.runtimeAnimatorController = usingBack ? backController : frontController;
        }

        // Left / Right flip
        if (!usingBack)
        {
            sprite.flipX = flat.x < 0f;
        }
        else
        {
            sprite.flipX = flat.x > 0f; // mirror correctly for back
        }
    }
}
