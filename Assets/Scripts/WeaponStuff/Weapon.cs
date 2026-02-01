using UnityEngine;

[System.Serializable]
public abstract class Weapon
{
    [Header("Waepon Stats")]
    [SerializeField]
    protected float damage;
    protected float damageMultiplier = 1f;
    [SerializeField]
    protected float range;
    [SerializeField]
    protected float attackCooldown;
    [SerializeField]
    protected float lastAttackTime;

    protected Transform origin;
     public float DamageMultiplier => damageMultiplier;


    public float Damage
    {
        get => damage;
        set => damage = value;
    }

    public float Range
    {
        get => range;
        set => range = value;
    }

    public float AttackCooldown
    {
        get => attackCooldown;
        set => attackCooldown = value;
    }

    public virtual void Init()
    {
        lastAttackTime = -attackCooldown;
    }

    public abstract bool canAttack();

    public abstract void attack();

    public virtual bool TryApplyUpgrade(WeaponUpgradeType type, ModiferType modiferType, float value)
    {
        if (type == WeaponUpgradeType.Damage)
        {
            if (modiferType == ModiferType.Add)
            {
                Damage += value;
            }
            else if (modiferType == ModiferType.Multiply)
            {
                Damage *= value;
            }
            return true;
        }

        if (type == WeaponUpgradeType.AttackCooldown)
        {
            if (modiferType == ModiferType.Add)
            {
                AttackCooldown -= value; // Addition reduziert die Abklingzeit
            }
            else if (modiferType == ModiferType.Multiply)
            {
                AttackCooldown *= (1f - value);
            }
            return true;
        }

        return false;
    public virtual void AddDamageMultiplier(float amount)
    {
        damageMultiplier += amount;
        damageMultiplier = Mathf.Max(0.1f, damageMultiplier);
    }
}
}