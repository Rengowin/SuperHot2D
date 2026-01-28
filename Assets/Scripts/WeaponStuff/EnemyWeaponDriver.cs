using UnityEngine;

public class EnemyWeaponDriver : MonoBehaviour
{
    [SerializeField] WeaponController weapons;
    [SerializeField] Transform muzzle;
    [SerializeField] Transform target;

    [SerializeField] float fireRange = 12f;
    [SerializeField] float fireCooldown = 0.6f;

    private float nextFireTime;

    void Awake()
    {
        weapons = GetComponent<WeaponController>();
        if (weapons && muzzle) weapons.SetShootMuzzle(muzzle);
    }

    void Start()
    {
    }

    void Update()
    {
        if (!weapons || !muzzle || !target) return;

        Vector3 direction = target.position - muzzle.position;
        direction.y = 0f;
        weapons.SetAimDirection(direction);

        if (weapons.Current is Range range && range.Ammo <= 0)
            weapons.Reload();

        if (Vector3.Distance(muzzle.position, target.position) <= fireRange && Time.time >= nextFireTime)
        {
            weapons.PrimaryFire();
            nextFireTime = Time.time + fireCooldown;
        }
    }
}
