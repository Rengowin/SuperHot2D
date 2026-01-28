using UnityEngine;

public class WeaponLoadout : MonoBehaviour
{
    [SerializeField] WeaponController weapons;

    [Header("Starting Weapon")]
    [SerializeField] WeaponsEnum weaponType;
    [SerializeField] WeaponStats weaponStats;

    void Awake()
    {
        if (!weapons) weapons = GetComponent<WeaponController>();
    }

    void Start()
    {
        if (!weapons)
        {
            Debug.LogError($"{name}: WeaponController not found on same GameObject.");
            return;
        }

        Weapon myWeapon = MakeLoadOut.CreateWeapon(weaponType, weaponStats);
        if (myWeapon == null)
        {
            Debug.LogError($"{name}: MakeLoadOut returned null weapon.");
            return;
        }

        weapons.ClearWeapons();
        weapons.AddWeapon(myWeapon);
        weapons.Init();
    }
}
