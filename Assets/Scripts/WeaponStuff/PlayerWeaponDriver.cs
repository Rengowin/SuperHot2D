using NUnit.Framework;
using UnityEngine;

public class PlayerWeaponDriver : MonoBehaviour
{
    [SerializeField] WeaponController weapons;
    [SerializeField] Camera cam;
    [SerializeField] Transform muzzle;
    [SerializeField] float aimPlaneY = 0f;


    void Awake()
    {
        if (!weapons) weapons = GetComponent<WeaponController>();
        if (weapons && muzzle) weapons.SetShootMuzzle(muzzle);
    }

    void Update()
    {
        if (!cam) cam = Camera.main;
        if (!cam || !weapons || !muzzle) return;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, new Vector3(0f, aimPlaneY, 0f));

        if (plane.Raycast(ray, out float enter))
        {
            Vector3 aimPoint = ray.GetPoint(enter);
            Vector3 direction = aimPoint - muzzle.position;
            direction.y = 0f;
            weapons.SetAimDirection(direction);
        }

        if (Input.GetMouseButtonDown(0))
            weapons.PrimaryFire();

        if (Input.GetKeyDown(KeyCode.R))
            weapons.Reload();
    }
}
