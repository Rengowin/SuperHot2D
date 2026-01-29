using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    [Header("What to rotate")]
    [Tooltip("Assign the camera (or a pivot above it) you want to rotate left/right.")]
    [SerializeField] private Transform cameraToRotate;

    [Header("Mouse Look")]
    [SerializeField] private float sensitivity = 2f;

    [Header("Lock Cursor")]
    [SerializeField] private bool lockCursor = true;

    [Header("Optional")]
    [Tooltip("If true, keeps pitch/roll exactly as they are and only changes yaw.")]
    [SerializeField] private bool preservePitchRoll = true;

    private float yaw;

    void Start()
    {
        if (cameraToRotate == null)
        {
            Debug.LogError($"{name}: No cameraToRotate assigned.");
            enabled = false;
            return;
        }

        yaw = cameraToRotate.eulerAngles.y;

        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X");
        yaw += mouseX * sensitivity;

        if (preservePitchRoll)
        {
            Vector3 e = cameraToRotate.eulerAngles;
            cameraToRotate.rotation = Quaternion.Euler(e.x, yaw, e.z);
        }
        else
        {
            cameraToRotate.rotation = Quaternion.Euler(0f, yaw, 0f);
        }
    }
}

