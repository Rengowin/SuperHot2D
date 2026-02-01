using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class PlayerStatsUpgrades : MonoBehaviour
{
    GameObject player;
    PlayerStats playerStats;

    WeaponController weaponController;
    Weapon weapon;

    Movement movement;


    [SerializeField]
    List<UpgradeStats> upgradeStats;


    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        playerStats = player.GetComponent<PlayerStats>();
        weaponController = player.GetComponent<WeaponController>();
        movement = player.GetComponent<Movement>();
        weapon = weaponController.Current;
    }

    public void UpgradePlayerStats(PlayerUpgradeType upgradeType, ModiferType modiferType, float value)
    {
        switch (upgradeType)
        {
            case PlayerUpgradeType.Health:
                if (modiferType == ModiferType.Add)
                {
                    playerStats.MaxHP += value;
                    playerStats.HP += value;
                }
                else if (modiferType == ModiferType.Multiply)
                {
                    playerStats.MaxHP *= value;
                    playerStats.HP *= value;
                }
                break;

            case PlayerUpgradeType.MoveSpeed:
                if (modiferType == ModiferType.Add)
                {
                    movement.maxSpeed += value;
                }
                else if (modiferType == ModiferType.Multiply)
                {
                    movement.maxSpeed *= value;
                }
                break;

            default:
                Debug.Log("Invalid upgrade type for player stats.");
                break;
        }
    }

    public void UpgradeWeaponStats(WeaponUpgradeType upgradeType, ModiferType modiferType, float value)
    {
        if (weapon.TryApplyUpgrade(upgradeType, modiferType, value))
        {
            Debug.Log($"Upgrade applied: {upgradeType} with {modiferType} and value {value}");
        }
        else
        {
            Debug.LogWarning($"Upgrade failed: {upgradeType} is not supported by the current weapon.");
        }
    }
}
