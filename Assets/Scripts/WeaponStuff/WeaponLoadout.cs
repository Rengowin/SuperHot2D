using UnityEngine;
using System.Collections.Generic;

public class WeaponLoadout : MonoBehaviour
{
    [SerializeField] private WeaponController weapons;

    [Header("Starting Weapons")]
    [SerializeField] private List<WeaponsEnum> weaponTypes;
    [SerializeField] private List<WeaponStats> weaponStats;

    void Awake()
    {
        if (!weapons)
            weapons = GetComponent<WeaponController>();
    }

    void Start()
    {
        if (!weapons)
        {
            Debug.LogError($"{name}: WeaponController not found.");
            return;
        }

        weapons.ClearWeapons();

        for (int i = 0; i < weaponTypes.Count; i++)
        {
            if (i >= weaponStats.Count) break;

            Weapon weapon = MakeLoadOut.CreateWeapon(weaponTypes[i], weaponStats[i]);
            if (weapon != null)
                weapons.AddWeapon(weapon);
        }

        weapons.Init();
    }
}
