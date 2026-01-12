using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("Testing")]
    [SerializeField] int startIndex = 0;

    List<Weapon> weapons = new List<Weapon>();
    int currentIndex = 0;

    [SerializeField] Camera cam;
    [SerializeField] float rayDistance = 50f;

    [SerializeField] Enemy dummy;

    private Weapon Current => (weapons.Count > 0) ? weapons[currentIndex] : null;

    void Start()
    {
        weapons.Add(new Swords(10,1.5f,0.5f));
        weapons.Add(new Spear(15,2.0f,0.7f));
        weapons.Add(new Pistol(8,15.0f,1.0f,10));
        weapons.Add(new ShotGun(5,10.0f,1.5f,8,6));
        weapons.Add(new RocketLauncher(50,20.0f,2.5f,5,5.0f));
        weapons.Add(new Kamikadze());

        currentIndex = Mathf.Clamp(startIndex,0, weapons.Count -1);

        // Init the currently equipped weapon
        Current?.Init();

        Debug.Log($"Equipped: {Current.GetType().Name} (Index {currentIndex})");
    }

    void Update()
    {
        if (Current == null) return;

        // Attack auf Linksklick
        if (Input.GetMouseButtonDown(0))
        {
            // Attack ausführen
            Current.attack();

            // NEU: Raycast-Damage nur bei Range Waffen testen
            if (Current is Range)
            {
                TryRaycastDamage();
            }

            Debug.Log($"Ammo: {GetAmmoDebug(Current)}");
        }

        // Reload auf R (nur Range)
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (Current is Range r)
            {
                r.Reload();
                Debug.Log($"Reloaded. Ammo: {r.Ammo}/{r.MaxAmmo}");
            }
            else
            {
                Debug.Log("Current weapon has no Reload()");
            }
        }

        // Switch 1-9
        HandleNumberSwitch();
    }

    private void HandleNumberSwitch()
    {
        for (int i = 0; i < 9; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                EquipIndex(i);
            }
        }
    }

    private void EquipIndex(int index)
    {
        if (index < 0 || index >= weapons.Count)
        {
            Debug.Log($"No weapon in slot {index + 1}");
            return;
        }

        currentIndex = index;

        //Init bei jedem Waffenwechsel (setzt z.B. ammo voll)
        Current.Init();

        Debug.Log($"Equipped: {Current.GetType().Name} (Slot {currentIndex + 1})");
    }

    private string GetAmmoDebug(Weapon w)
    {
        if (w is Range r)
            return $"{r.Ammo}/{r.MaxAmmo}";
        return "n/a";
    }

    private void TryRaycastDamage()
    {
        if (cam == null) cam = Camera.main;
        if (cam == null) return;

        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance))
        {
            // Fürs Testen: dummy fest, passt für dich
            if (dummy != null)
            {
                dummy.HP -= Current.Damage;
            }

            Debug.Log("Hit: " + hit.collider.name);
        }
    }
}
