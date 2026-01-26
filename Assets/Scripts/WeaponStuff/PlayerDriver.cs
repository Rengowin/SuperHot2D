using UnityEngine;

public class PlayerWeaponDriver : MonoBehaviour
{
    [SerializeField] private WeaponController weapons;
    [SerializeField] private Camera cam;
    [SerializeField] private Transform muzzle;
    [SerializeField] private float aimPlaneY = 0f; // Bodenhöhe

    void Update()
    {
        if (!cam) cam = Camera.main;
        if (!cam || !weapons || !muzzle) return;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, new Vector3(0f, aimPlaneY, 0f));

        if (plane.Raycast(ray, out float enter))
        {
            Vector3 aimPoint = ray.GetPoint(enter);
            Vector3 dir = aimPoint - muzzle.position;
            dir.y = 0f;
            weapons.SetAimDirection(dir);
        }

        if (Input.GetMouseButtonDown(0))
            weapons.PrimaryFire();

        if (Input.GetKeyDown(KeyCode.R))
            weapons.Reload();
    }
}
