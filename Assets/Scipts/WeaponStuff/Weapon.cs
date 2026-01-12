using UnityEngine;

[System.Serializable]
public abstract class Weapon
{
    [Header("Waepon Stats")]
    [SerializeField]
    protected float damage;
    [SerializeField]
    protected float range;
    [SerializeField]
    protected float attackCooldown;
    [SerializeField]
    protected float lastAttackTime;

    public float Damage { get { return damage; } }
    public float Range { get { return range; } }

    public virtual void Init()
    {
        lastAttackTime = -attackCooldown;
    }

    public abstract bool canAttack();

    public abstract void attack();
}
