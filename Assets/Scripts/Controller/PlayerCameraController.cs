using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCameraController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera topDownCamera;
    [SerializeField] private LayerMask groundMask;

    [Header("Rotation")]
    [SerializeField] private float rotationSpeed = 20f;

    void Awake()
    {
        // Auto-assign camera if not set
        if (topDownCamera == null)
            topDownCamera = Camera.main;
    }

    void Update()
    {
        // Safety checks
        if (topDownCamera == null)
            return;

        if (Mouse.current == null)
            return;

        // 1. Ray from top-down camera through mouse
        Ray ray = topDownCamera.ScreenPointToRay(
            Mouse.current.position.ReadValue()
        );

        // 2. Hit the ground (or any aim plane)
        if (Physics.Raycast(ray, out RaycastHit hit, 500f, groundMask))
        {
            Vector3 lookPoint = hit.point;

            // 3. Direction on X/Z plane only
            Vector3 dir = lookPoint - transform.position;
            dir.y = 0f;

            if (dir.sqrMagnitude < 0.001f)
                return;

            // 4. Rotate smoothly
            Quaternion targetRot = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRot,
                rotationSpeed * Time.deltaTime
            );
        }
    }
}
