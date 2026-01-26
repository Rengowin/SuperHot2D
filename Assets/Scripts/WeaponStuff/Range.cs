using Unity.VisualScripting;
using UnityEngine;
using static Bullet;

public abstract class Range : Weapon
{
    protected float ammo;
    protected float maxAmmo;

    protected Transform muzzel;
    protected GameObject projectilePrefab;

    public float MaxAmmo => maxAmmo;
    public float Ammo => ammo;

    public Vector3 direction;

    public GameObject ProjectilePrefab => projectilePrefab;

    public Transform Muzzel
    {
        get => muzzel;
        set => muzzel = value;
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
    public void attack(Vector3 aimDir)
    {
        if (canAttack())
        {
            Shoot(aimDir.normalized);
            lastAttackTime = Time.time;
            ammo--;
        }
    }
    public abstract void Shoot(Vector3 aimDir);

    public virtual void Reload()
    {
        ammo = maxAmmo;
        Debug.Log("Weapon reloaded!");
    }

    public void SpawnBullet(Vector3 dir, GameObject prefab, BulletInit init)
    {
        if (muzzel == null) { Debug.LogError("No muzzle set"); return; }
        if (prefab == null) { Debug.LogError("No prefab"); return; }

        dir = dir.normalized;
        var go = Object.Instantiate(prefab, muzzel.position, Quaternion.LookRotation(dir));
        var b = go.GetComponent<Bullet>();
        b.Init(init, dir);
    }

}