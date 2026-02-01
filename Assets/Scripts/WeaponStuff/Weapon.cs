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


    public float Damage { get { return damage; } }
    public float Range { get { return range; } }

    public virtual void Init()
    {
        lastAttackTime = -attackCooldown;
    }

    public abstract bool canAttack();

    public abstract void attack();

    public virtual void AddDamageMultiplier(float amount)
    {
        damageMultiplier += amount;
        damageMultiplier = Mathf.Max(0.1f, damageMultiplier);
    }
}