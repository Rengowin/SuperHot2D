using UnityEngine;

[System.Serializable]
public abstract class Weapon : MonoBehaviour
{
    [Header("Waepon Stats")]
    [SerializeField]
    float damage;
    [SerializeField]
    float range;
    [SerializeField]
    float attackCooldown;
    public DamageSource owner;
    public WeaponType weaponType;


    public abstract bool canAttack();

    public abstract void attack();

    protected DamageInfo CreateDamageInfo()
    {
        return new DamageInfo
        {
            damageAmount = damage,
            source = owner,
            weaponType = weaponType,
            attacker = gameObject
        };
    }
}
