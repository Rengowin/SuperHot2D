using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("Projectiles")]
    [SerializeField] private GameObject defaultBulletPrefab;

    [Header("Runtime")]
    [SerializeField] private int startIndex = 0;

    private readonly List<Weapon> weapons = new List<Weapon>();
    private int currentIndex;

    private Transform shootMuzzle;
    private Transform meleeOrigin;

    private Vector3 aimDir = Vector3.forward;

    public Weapon Current => (weapons.Count > 0) ? weapons[currentIndex] : null;

    public void SetShootMuzzle(Transform t) => shootMuzzle = t;
    public void SetMeleeOrigin(Transform t) => meleeOrigin = t;

    public void SetAimDirection(Vector3 dir)
    {
        if (dir.sqrMagnitude > 0.0001f) aimDir = dir.normalized;
    }

    public void ClearWeapons()
    {
        weapons.Clear();
        currentIndex = 0;
    }

    public void AddWeapon(Weapon w)
    {
        if (w == null) return;
        weapons.Add(w);
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
        var w = Current;
        if (w == null) return;

        ApplyRuntimeRefs(w);

        if (w is Range r) r.attack(aimDir);
        else if (w is Melee m) m.attack();
        else w.attack();
    }

    public void Reload()
    {
        if (Current is Range r) r.Reload();
    }

    private void ApplyRuntimeRefs(Weapon w)
    {
        if (w is Range r)
        {
            if (shootMuzzle == null)
            {
                Debug.LogWarning($"{name}: shootMuzzle not set. Using transform.");
                shootMuzzle = transform;
            }

            r.Muzzel = shootMuzzle;

            if (r.ProjectilePrefab == null && defaultBulletPrefab != null)
                r.SetProjectilePrefab(defaultBulletPrefab);
        }

    }
}
