using UnityEngine;

public class EnemyWeaponDriver : MonoBehaviour
{
    [SerializeField] private WeaponController weapons;
    [SerializeField] private Transform muzzle;
    [SerializeField] private Transform target;

    [SerializeField] private float fireRange = 12f;
    [SerializeField] private float fireCooldown = 0.6f;

    private float nextFireTime;

    void Start()
    {
        if (!weapons) weapons = GetComponent<WeaponController>();
        if (weapons && muzzle) weapons.SetShootMuzzle(muzzle);

        if (!target)
        {
            var p = GameObject.FindGameObjectWithTag("Player");
            if (p) target = p.transform;
        }
    }

    void Update()
    {
        if (!weapons || !muzzle || !target) return;

        Vector3 dir = target.position - muzzle.position;
        dir.y = 0f;
        weapons.SetAimDirection(dir);

        if (weapons.Current is Range r && r.Ammo <= 0)
            weapons.Reload();

        if (Vector3.Distance(muzzle.position, target.position) <= fireRange && Time.time >= nextFireTime)
        {
            weapons.PrimaryFire();
            nextFireTime = Time.time + fireCooldown;
        }
    }
}
