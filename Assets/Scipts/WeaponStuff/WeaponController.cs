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

    private Weapon Current => (weapons.Count > 0) ? weapons[currentIndex] : null;

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform muzzle;
    [SerializeField] float bulletSpeed = 25f;

    void Start()
    {
        weapons.Add(new Swords(10,1.5f,0.5f,160.0f));
        weapons.Add(new Spear(15,2.0f,0.7f,0.3f));
        weapons.Add(new Pistol(8,15.0f,1.0f,10));
        weapons.Add(new ShotGun(5,6.0f,10.0f,1.5f,8,6));
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
            // Attack ausf�hren
            Current.attack();

            if (Current is ShotGun sg)
            {
                TryShotgunDamageAndVisuals(sg);
            }
            else if (Current is Range)
            {
                TryRaycastDamage();
            }
            if (Current is Swords sw)
            {
                DoSwordSwing(sw);
            }
            else if (Current is Spear sp)
            {
                DoSpearThrust(sp);
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
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1)) EquipIndex(0);
        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2)) EquipIndex(1);
        if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3)) EquipIndex(2);
        if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4)) EquipIndex(3);
        if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5)) EquipIndex(4);
        if (Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Keypad6)) EquipIndex(5);
        if (Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Keypad7)) EquipIndex(6);
        if (Input.GetKeyDown(KeyCode.Alpha8) || Input.GetKeyDown(KeyCode.Keypad8)) EquipIndex(7);
        if (Input.GetKeyDown(KeyCode.Alpha9) || Input.GetKeyDown(KeyCode.Keypad9)) EquipIndex(8);
    }

    private void EquipIndex(int index)
    {
        if (index < 0 || index >= weapons.Count)
        {
            Debug.Log($"No weapon in slot {index + 1}");
            return;
        }

        currentIndex = index;

        Current?.Init();

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

        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f));

        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance))
        {
            Vector3 targetPoint = hit.point;
            SpawnBullet(targetPoint);

            Enemy enemy = hit.collider.GetComponentInParent<Enemy>();
            if (enemy != null && !(Current is RocketLauncher))
            {
                enemy.HP -= Current.Damage;
                Debug.Log($"Direct hit on {enemy.name}, -{Current.Damage} HP");
            }

            Debug.Log("Hit: " + hit.collider.name);
        }
        else
        {
            Vector3 targetPoint = cam.transform.position + cam.transform.forward * rayDistance;
            SpawnBullet(targetPoint);
        }
    }


    private void SpawnBullet(Vector3 targetPoint)
    {
        if (bulletPrefab == null || muzzle == null) return;

        GameObject b = Instantiate(bulletPrefab, muzzle.position, Quaternion.identity);

        Bullet bullet = b.GetComponent<Bullet>();
        if (bullet != null)
        {
            if (Current is RocketLauncher rocket)
            {
                bullet.isExplosive = true;
                bullet.explosionRadius = rocket.BlastRadius;   // wenn du die Property ergänzt
                bullet.explosionDamage = Current.Damage;       // oder *2f etc.
            }
            else
            {
                bullet.isExplosive = false;
            }
        }

        Rigidbody rb = b.GetComponent<Rigidbody>();
        Vector3 dir = (targetPoint - muzzle.position).normalized;

        if (rb != null)
            rb.linearVelocity = dir * bulletSpeed;
    }

    private Vector3 ApplySpread(Vector3 direction, float spreadAngleDeg)
    {
        float yaw = Random.Range(-spreadAngleDeg, spreadAngleDeg);
        float pitch = Random.Range(-spreadAngleDeg, spreadAngleDeg);
        return Quaternion.Euler(pitch, yaw, 0f) * direction;
    }

    private void TryShotgunDamageAndVisuals(ShotGun sg)
    {
        if (cam == null) cam = Camera.main;
        if (cam == null) return;

        int pellets = sg.Pellets;
        float spread = sg.SpreadAngle;

        // Ray aus Screen-Mitte
        Ray baseRay = cam.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f));
        Vector3 origin = baseRay.origin;

        for (int i = 0; i < pellets; i++)
        {
            Vector3 dir = ApplySpread(baseRay.direction, spread);

            if (Physics.Raycast(origin, dir, out RaycastHit hit, rayDistance))
            {
                SpawnBullet(hit.point);

                Enemy enemy = hit.collider.GetComponentInParent<Enemy>();
                if (enemy != null)
                {
                    enemy.HP -= Current.Damage; // pro Pellet Damage
                }
            }
            else
            {
                SpawnBullet(origin + dir * rayDistance);
            }
        }
    }

    private void DoSwordSwing(Swords sw)
    {
        Transform baseT = (muzzle != null) ? muzzle : transform;

        Vector3 originPos = baseT.position;

        float radius = Current.Range;
        float halfAngle = sw.SwingAngle * 0.5f;

        Collider[] hits = Physics.OverlapSphere(originPos, radius);
        var damaged = new System.Collections.Generic.HashSet<Enemy>();

        Vector3 forward = baseT.forward;
        forward.y = 0f;
        forward.Normalize();

        foreach (var h in hits)
        {
            Enemy enemy = h.GetComponentInParent<Enemy>();
            if (enemy == null || !damaged.Add(enemy)) continue;

            // ✅ ClosestPoint statt enemy.transform.position
            Vector3 closest = h.ClosestPoint(originPos);
            Vector3 toEnemy = closest - originPos;
            toEnemy.y = 0f;

            if (toEnemy.sqrMagnitude < 0.0001f)
            {
                // Wenn du wirklich "drin stehst": zählt als Hit
                enemy.HP -= Current.Damage;
                continue;
            }

            float angle = Vector3.Angle(forward, toEnemy);
            if (angle <= halfAngle)
            {
                enemy.HP -= Current.Damage;
            }
        }
    }


    private void DoSpearThrust(Spear sp)
    {
        if (muzzle == null) return;

        float distance = Current.Range;
        float radius = sp.ThrustRadius;

        // optional: minimal nach vorne, damit du nicht "im Collider" startest
        Vector3 origin = muzzle.position + muzzle.forward * 0.05f;
        Ray ray = new Ray(origin, muzzle.forward);

        RaycastHit[] hits = Physics.SphereCastAll(ray, radius, distance);

        // Sortieren: vorne -> hinten (für konsistente Logs)
        System.Array.Sort(hits, (a, b) => a.distance.CompareTo(b.distance));

        var damaged = new System.Collections.Generic.HashSet<Enemy>();

        foreach (var h in hits)
        {
            Enemy enemy = h.collider.GetComponentInParent<Enemy>();
            if (enemy == null) continue;

            // pro Enemy nur einmal (falls mehrere Collider)
            if (!damaged.Add(enemy)) continue;

            enemy.HP -= Current.Damage;
            Debug.Log($"Pierce hit: {enemy.name} at {h.distance}");
        }
    }


}
