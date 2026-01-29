using UnityEngine;

public class PlayerWeaponDriver : MonoBehaviour
{
    [SerializeField] private WeaponController weapons;
    [SerializeField] private Transform muzzle;
    [SerializeField] private Transform weaponPivot;

    [Header("Aim Plane")]
    [SerializeField] private float aimPlaneY = 0f;
    [SerializeField] private float rotationSpeed = 25f;

    void Awake()
    {
        if (!weapons) weapons = GetComponent<WeaponController>();
        if (weapons && muzzle) weapons.SetShootMuzzle(muzzle);

        if (weaponPivot == null) weaponPivot = muzzle;
    }

    void Update()
    {
        if (!weapons || !muzzle) return;

        UpdateAimFromMousePlane();

        if (Input.GetMouseButtonDown(0))
            weapons.PrimaryFire();

        if (Input.GetKeyDown(KeyCode.R))
            weapons.Reload();
    }

    void UpdateAimFromMousePlane()
    {
        Camera cam = Camera.main;
        if (!cam) return;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, new Vector3(0f, aimPlaneY, 0f)); // world Y plane

        if (!plane.Raycast(ray, out float enter))
            return;

        Vector3 point = ray.GetPoint(enter);

        Vector3 dir = point - muzzle.position;
        dir.y = 0f;

        if (dir.sqrMagnitude < 0.0001f)
            return;

        weapons.SetAimDirection(dir);

        // rotate muzzle/pivot to face aim
        Quaternion target = Quaternion.LookRotation(dir.normalized, Vector3.up);
        weaponPivot.rotation = Quaternion.Slerp(weaponPivot.rotation, target, rotationSpeed * Time.deltaTime);
    }
}
