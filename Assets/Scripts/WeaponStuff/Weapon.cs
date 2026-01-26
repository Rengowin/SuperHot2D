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

    protected Transform origin;
    public Transform Origin
    {
        get => origin;
        set => origin = value;
    }


    public float Damage { get { return damage; } }
    public float Range { get { return range; } }

    public virtual void Init()
    {
        lastAttackTime = -attackCooldown;
    }

    public abstract bool canAttack();

    public abstract void attack();
}