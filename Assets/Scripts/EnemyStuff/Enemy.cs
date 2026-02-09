using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    float hp, movementSpeed;
    [SerializeField]
    int damage;
    //[SerializeField]
    //Weapon weapon;

    float HP
    {
        get { return hp; }
        set { hp = value;}
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
