using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
[Header("Stats")]
    [SerializeField] private float hp;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float damage;

    [Header("Weapon")]
    [SerializeField] private Weapon weapon;

    public Action onDeath;

    // --- Properties (internal use) ---
    float HP
    {
        get { return hp; }
        set { hp = Mathf.Max(0, value); }
    }

    float MovementSpeed
    {
        get { return movementSpeed; }
        set { movementSpeed = Mathf.Max(0, value); }
    }

    float Damage
    {
        get { return damage; }
        set { damage = Mathf.Max(1, value); }
    }

    // --- Initialization from EnemySpawnInfos ---
    public void Init(EnemySpawnInfos infos)
    {
        HP = infos.HP;
        MovementSpeed = infos.MovementSpeed;
        Damage = infos.Damage;
        weapon = infos.Weapon;

        EquipWeapon();
    }

    void EquipWeapon()
    {
        if (weapon == null)
            return;

        Weapon spawnedWeapon = Instantiate(weapon, transform);
        // Optional:
        // spawnedWeapon.SetOwner(this);
        // spawnedWeapon.SetDamage(Damage);
    }

    public void TakeDamage(float amount)
    {
        HP -= amount;

        if (HP <= 0)
            Die();
    }

    public void Die()
    {
        onDeath?.Invoke();
        Destroy(gameObject);
    }

    /* we can use the equip weapon above
    
    public void init(float hp, float movementSpeed, int damage, Weapon weapon)
    {
        HP = hp;
        MovementSpeed = movementSpeed;
        Damage = damage;

        //this.weapon = weapon;
    }*/ 
}
