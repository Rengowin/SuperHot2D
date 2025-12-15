using UnityEngine;

[System.Serializable]
public abstract class Weapon
{
    [Header("Waepon Stats")]
    [SerializeField]
    float damage;
    [SerializeField]
    float range;
    [SerializeField]
    float attackCooldown;


    public abstract bool canAttack();

    public abstract void attack();
}
