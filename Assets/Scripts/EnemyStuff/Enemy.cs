using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    float hp, movementSpeed, damage;
    //[SerializeField]
    //Weapon weapon;


    //we could add if contions for to low values like hp below 0 to this or if dmg would go below 1
    public float HP
    {
        get { return hp; }
        set { hp = value;
            Debug.Log($"Enemy HP set to: {hp}");
            if (hp <= 0)
            {
                Die();
            }
        }
    }

    float MovementSpeed
    {
        get { return movementSpeed; }
        set { movementSpeed = value; }
    }

    float Damage
    {
        get { return damage; }
        set { damage = value; }
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void init(float hp, float movementSpeed, int damage, Weapon weapon)
    {
        HP = hp;
        MovementSpeed = movementSpeed;
        Damage = damage;

        //this.weapon = weapon;
    }

    public void Die()
    {
        Debug.Log("Enemy died");
        Destroy(this.gameObject);
    }
}