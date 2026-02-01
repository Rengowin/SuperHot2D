using Unity.VisualScripting;
using UnityEngine;
using static Bullet;

public abstract class Range : Weapon
{
    protected float ammo;
    protected float maxAmmo;

    protected float bulletSpeed;

    protected Transform muzzel;
    protected GameObject projectilePrefab;

    protected int bulletsPerShot;

    public float MaxAmmo
    {
        get => maxAmmo;
        set => maxAmmo = value;
    }

    public float Ammo
    {
        get => ammo;
        set => ammo = value;
    }

    public float BulletSpeed
    {
        get => bulletSpeed;
        set => bulletSpeed = value;
    }

    public Vector3 direction;

    public GameObject ProjectilePrefab
    {
        get => projectilePrefab;
    }

    public Transform Muzzel
    {
        get => muzzel;
        set => muzzel = value;
    }

    public int BulletsPerShot
    {
        get => bulletsPerShot;
        set => bulletsPerShot = value;
    }

    public override void Init()
    {
        base.Init();
        ammo = maxAmmo;
    }


    // must be called form player or the enemys or something im not sure or maybe in weapon Controller where rightnow also is the bullet prefab
    //nop i desiced to for now that the weapom thats whants to shoot must look first if it has a prefab else it will open the setProjectilePrefab method
    public void SetProjectilePrefab(GameObject prefab)
    {
        projectilePrefab = prefab;
    }

    public override bool canAttack()
    {
        if (ammo > 0)
        {
            return Time.time >= lastAttackTime + attackCooldown;
        }
        else
        {
            Debug.Log("Out of ammo!");
            return false;
        }
    }

    public override void attack()
    {
        attack(direction);
    }
    public void attack(Vector3 aimDirection)
    {
        if (canAttack())
        {
            Shoot(aimDirection.normalized);
            lastAttackTime = Time.time;
            ammo--;
        }
    }
    public abstract void Shoot(Vector3 aimDirection);

    public virtual void Reload()
    {
        ammo = maxAmmo;
        Debug.Log("Weapon reloaded!");
    }

    public void SpawnBullet(Vector3 direction, GameObject prefab, BulletInit init)
{
    if (muzzel == null) { Debug.LogError("No muzzle set"); return; }
    if (prefab == null) { Debug.LogError("No prefab"); return; }

    direction = direction.normalized;

    var go = Object.Instantiate(prefab, muzzel.position, Quaternion.LookRotation(direction));
    var b = go.GetComponent<Bullet>();

    if (b != null)
    {
        // ✅ Set the owner so bullets don't damage the shooter
        b.owner = muzzel.root.gameObject;   // shooter (player/enemy root)
        b.Init(init, direction);
    }
    else
    {
        Debug.LogError("Spawned projectile has no Bullet component!");
    }
}

    public void addAmmo(float amnt)
    {
        ammo += amnt;
        if (ammo > maxAmmo) ammo = maxAmmo;
    }

    // Aktualisiere TryApplyUpgrade, um ModiferType zu berücksichtigen
    public override bool TryApplyUpgrade(WeaponUpgradeType type, ModiferType modiferType, float value)
{
    if (base.TryApplyUpgrade(type, modiferType, value)) { return true; }

    if (type == WeaponUpgradeType.MaxAmmo)
    {
        if (modiferType == ModiferType.Add)
        {
        MaxAmmo += Mathf.RoundToInt(value);
        Ammo = Mathf.Min(Ammo, MaxAmmo);
        }
    else if (modiferType == ModiferType.Multiply)
        {
        MaxAmmo = Mathf.RoundToInt(MaxAmmo * value);
        Ammo = Mathf.Min(Ammo, MaxAmmo);
    }
        return true;
    }

    if (type == WeaponUpgradeType.BulletSpeed)
    {
        if (modiferType == ModiferType.Add)
        {
       BulletSpeed += value;
  }
   else if (modiferType == ModiferType.Multiply)
    {
  BulletSpeed *= value;
    }
        return true;
    }

    if (type == WeaponUpgradeType.Range)
    {
    if (modiferType == ModiferType.Add)
    {
        range += value;
        }
        else if (modiferType == ModiferType.Multiply)
   {
   range *= value;
    }
    }

    return false;
 }



}