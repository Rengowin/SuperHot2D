using UnityEngine;

public class WeaponLoadout : MonoBehaviour
{
    [SerializeField] WeaponController weapons;

    [Header("Starting Weapon")]
    [SerializeField] WeaponsEnum weaponType;
    [SerializeField] WeaponStats weaponStats;

    [Header("Current Weapon (Runtime)")]
    public Weapon currentWeapon; // Sichtbar im Inspector

    [Header("Current Weapon Stats (Runtime)")]
    public float currentDamage;
    public float currentRange;
    public float currentAttackCooldown;

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

        currentWeapon = MakeLoadOut.CreateWeapon(weaponType, weaponStats);
        if (currentWeapon == null)
        {
            Debug.LogError($"{name}: MakeLoadOut returned null weapon.");
            return;
        }

        weapons.ClearWeapons();
        weapons.AddWeapon(currentWeapon);
        weapons.Init();
    }

    void Update()
    {
        if (currentWeapon != null)
        {
            // Synchronisiere die Werte der aktuellen Waffe mit dem WeaponController
            Weapon activeWeapon = weapons.Current;
            if (activeWeapon != null && activeWeapon != currentWeapon)
            {
                weapons.ClearWeapons();
                weapons.AddWeapon(currentWeapon);
                weapons.Init();
            }
        }

        if (weapons.Current != null)
        {
            // Update the current weapon stats in the Inspector
            currentDamage = weapons.Current.Damage;
            currentRange = weapons.Current.Range;
            currentAttackCooldown = weapons.Current.AttackCooldown;
        }
    }
}
