using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class PlayerStatsUpgrades : MonoBehaviour
{
    GameObject player;
    PlayerStats playerStats;

    WeaponController weaponController;

    Movement movement;


    [SerializeField]
    List<UpgradeStats> upgradeStats;


    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        playerStats = player.GetComponent<PlayerStats>();
        weaponController = player.GetComponent<WeaponController>();
        movement = player.GetComponent<Movement>();
    }

    public void UpgradePlayerStats(PlayerUpgradeType upgradeType, ModiferType modiferType, float value)
    {
        switch (upgradeType)
        {
            case PlayerUpgradeType.MaxHealth:
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
        foreach (var weapon in weaponController.Weapons)
        {
            if (weapon.TryApplyUpgrade(upgradeType, modiferType, value))
            {
                Debug.Log($"Upgrade applied to weapon: {upgradeType} with {modiferType} and value {value}");
            }
            else
            {
                Debug.LogWarning($"Upgrade failed: {upgradeType} is not supported by the weapon.");
            }
        }
    }

    public List<UpgradeStats> GetRandomUpgrades(int count)
    {
        List<UpgradeStats> selectedUpgrades = new List<UpgradeStats>();
        List<UpgradeStats> availableUpgrades = new List<UpgradeStats>(upgradeStats);
        for (int i = 0; i < count; i++)
        {
            if (availableUpgrades.Count == 0)
                break;
            int randomIndex = Random.Range(0, availableUpgrades.Count);
            selectedUpgrades.Add(availableUpgrades[randomIndex]);
            availableUpgrades.RemoveAt(randomIndex);
        }
        return selectedUpgrades;
    }

    public void ApplyUpgrade(UpgradeStats upgrade)
    {
        if (upgrade == null) return;

        if (upgrade.Target == UpgradeTarget.Player)
            UpgradePlayerStats(upgrade.PlayerType, upgrade.ModiferType, upgrade.Value);
        else
            UpgradeWeaponStats(upgrade.WeaponType, upgrade.ModiferType, upgrade.Value);
    }

}
