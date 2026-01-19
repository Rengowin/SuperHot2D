using UnityEngine;

[System.Serializable]
public class EnemySpawnInfos
{
    [Header("Enemy Stats")]
    [SerializeField]
    float hp;
    [SerializeField]
    float movementSpeed;
    [SerializeField]
    float damage;

    [Header("Weapon Infos")]
    [SerializeField]
    Weapon weapon;

    //getters

    public float HP { get { return hp; } }
    public float MovementSpeed { get { return movementSpeed; } }
    public float Damage { get { return damage; } }
    public Weapon Weapon { get { return weapon; } }

}
