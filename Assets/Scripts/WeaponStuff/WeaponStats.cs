using UnityEngine;

[System.Serializable]
public class WeaponStats
{
    [Header("General Attributes")]
    [SerializeField]
    float damage;
    [SerializeField]
    float range;
    [SerializeField]
    float cooldown;
    [SerializeField]
    int ammo;
    [SerializeField]
    float bulletSpeed;

    [Header("Special Attributes")]
    [Header("Shotgun Settings")]
    [SerializeField]
    int pelletCount;
    [SerializeField]
    float spread;
    [Header("Rocket Launcher Settings")]
    [SerializeField]
    float explosionRadius;

    [Header("Melee Settings Special")]
    [Header("Sword Settings")]
    [SerializeField]
    float swingAngle;

    [Header("Spear Settings")]
    [SerializeField]
    float thrustRadius;

    public WeaponStats(float damage, float range, float cooldown, int ammo, float bulletSpeedl, int pelletCount = 0, float spread = 0f, float explosionRadius = 0f, float swingAngle = 0f, float thrustRadius = 0f)
    {
        this.damage = damage;
        this.range = range;
        this.cooldown = cooldown;
        this.ammo = ammo;
        this.bulletSpeed = bulletSpeedl;
        this.pelletCount = pelletCount;
        this.spread = spread;
        this.explosionRadius = explosionRadius;
        this.swingAngle = swingAngle;
        this.thrustRadius = thrustRadius;
    }

    public float Damage { get => damage; set => damage = value; }
    public float Range { get => range; set => range = value; }
    public float Cooldown { get => cooldown; set => cooldown = value; }
    public int Ammo { get => ammo; set => ammo = value; }
    public int PelletCount { get => pelletCount; set => pelletCount = value; }
    public float Spread { get => spread; set => spread = value; }
    public float ExplosionRadius { get => explosionRadius; set => explosionRadius = value; }
    public float SwingAngle { get => swingAngle; set => swingAngle = value; }
    public float ThrustRadius { get => thrustRadius; set => thrustRadius = value; }

    public float BulletSpeed { get => bulletSpeed; set => bulletSpeed = value; }


}
