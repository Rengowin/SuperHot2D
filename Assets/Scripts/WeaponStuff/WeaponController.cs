using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("Loadout (temporär zum Testen)")]
    [SerializeField] private int startIndex = 0;

    [Header("Scene References")]
    [SerializeField] private Transform muzzle;
    [SerializeField] private GameObject defaultBulletPrefab;

    [Header("Runtime")]
    [SerializeField] private List<Weapon> weapons = new List<Weapon>();
    private int currentIndex;
    private Vector3 aimDir = Vector3.forward;

    public Weapon Current => (weapons.Count > 0) ? weapons[currentIndex] : null;

    void Start()
    {
        weapons.Add(new Pistol(10, 200f, 1f, 15)); // (damage, range, cooldown, maxAmmo) – check deine Reihenfolge!

        currentIndex = Mathf.Clamp(startIndex, 0, weapons.Count - 1);

        Current?.Init();
        ApplyRuntimeRefs(Current);
    }


    public void SetAimDirection(Vector3 dir)
    {
        if (dir.sqrMagnitude > 0.0001f)
            aimDir = dir.normalized;
    }

    public void PrimaryFire()
    {
        var w = Current;
        if (w == null) return;

        if (w is Range r)
        {
            ApplyRuntimeRefs(r);
            r.attack(aimDir);     // Range braucht aimDir
        }
        else
        {
            w.attack();           // Melee nutzt attack()
        }
    }

    public void Reload()
    {
        if (Current is Range r)
            r.Reload();
    }

    public void EquipIndex(int index)
    {
        if (index < 0 || index >= weapons.Count) return;

        currentIndex = index;
        Current?.Init();
        ApplyRuntimeRefs(Current);
    }

    private void ApplyRuntimeRefs(Weapon w)
    {
        if (w is Range r)
            ApplyRuntimeRefs(r);
    }

    private void ApplyRuntimeRefs(Range r)
    {
        // Muzzle setzen
        if (r.Muzzel == null)
            r.Muzzel = muzzle;

        // Bullet prefab setzen
        if (r.ProjectilePrefab == null)
            r.SetProjectilePrefab(defaultBulletPrefab);
    }
}
