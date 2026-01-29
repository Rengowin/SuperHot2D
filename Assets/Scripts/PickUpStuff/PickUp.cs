using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class PickUp : MonoBehaviour
{
    GameObject player;
    PlayerStats playerStats;

    WeaponController weaponController;

    [SerializeField]
    PickUpStats pickUpStats;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        playerStats = player.GetComponent<PlayerStats>();
        weaponController = player.GetComponent<WeaponController>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PickUpItem()
    {
        switch (pickUpStats.PickUpType)
        {
            case PickUpEnum.Heal:
                Heal();
                Debug.Log("Picked up a Heal item.");
                break;
            case PickUpEnum.Ammo:
                Ammo();
                Debug.Log("Picked up an Ammo item.");
                break;
            default:
                Debug.Log("Unknown item picked up.");
                break;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            // For demonstration, let's assume this pick-up is of type Heal
            PickUpItem();
            Destroy(gameObject);
        }
    }

    public void Heal()
    {
        playerStats.Heal(pickUpStats.Amont);
    }

    public void Ammo()
    {
        weaponController.addAmmo(pickUpStats.Amont);
    }
}
