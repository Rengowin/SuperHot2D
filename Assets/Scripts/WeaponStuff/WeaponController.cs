using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("Projectiles")]
    [SerializeField] GameObject defaultBulletPrefab;

    [Header("Runtime")]
    [SerializeField] int startIndex = 0;

    readonly List<Weapon> weapons = new List<Weapon>();
    int currentIndex;

    Transform shootMuzzle;
    Transform meleeOrigin;

    Vector3 aimDir = Vector3.forward;

    public Weapon Current => (weapons.Count > 0) ? weapons[currentIndex] : null;

    public void SetShootMuzzle(Transform t) => shootMuzzle = t;
    public void SetMeleeOrigin(Transform t) => meleeOrigin = t;

    public void SetAimDirection(Vector3 direction)
    {
        if (direction.sqrMagnitude > 0.0001f) aimDir = direction.normalized;
    }

    public void ClearWeapons()
    {
        weapons.Clear();
        currentIndex = 0;
    }

    public void AddWeapon(Weapon weapon)
    {
        if (weapon == null) return;
        weapons.Add(weapon);
    }

    public void Init()
    {
        if (weapons.Count == 0)
        {
            Debug.LogWarning($"{name}: No weapons assigned.");
            return;
        }

        currentIndex = Mathf.Clamp(startIndex, 0, weapons.Count - 1);
        Current?.Init();
        ApplyRuntimeRefs(Current);
    }

    public void EquipIndex(int index)
    {
        if (index < 0 || index >= weapons.Count) return;
        currentIndex = index;
        Current?.Init();
        ApplyRuntimeRefs(Current);
    }

    public void PrimaryFire()
    {
        var weapon = Current;
        if (weapon == null) return;

        ApplyRuntimeRefs(weapon);

        if (weapon is Range range) range.attack(aimDir);
        else if (weapon is Melee melee) melee.attack();
        else weapon.attack();
    }

    public void Reload()
    {
        if (Current is Range range) range.Reload();
    }

    private void ApplyRuntimeRefs(Weapon weapon)
    {
        if (weapon is Range range)
        {
            if (shootMuzzle == null)
            {
                Debug.LogWarning($"{name}: shootMuzzle not set. Using transform.");
                shootMuzzle = transform;
            }

            range.Muzzel = shootMuzzle;


            if (range.ProjectilePrefab == null && defaultBulletPrefab != null)
                range.SetProjectilePrefab(defaultBulletPrefab);
        }
        else if (weapon is Melee melee)
        {
            if (meleeOrigin == null)
            {
                Debug.LogWarning($"{name}: meleeOrigin not set. Using transform.");
                meleeOrigin = transform;
            }
            melee.MeleeOrigin = meleeOrigin;
        }

    }

    public void addAmmo(float amont)
    {
        foreach (var weapon in weapons)
        {
            if (weapon is Range range)
            {
                range.addAmmo(amont);
            }
        }
    }
}
