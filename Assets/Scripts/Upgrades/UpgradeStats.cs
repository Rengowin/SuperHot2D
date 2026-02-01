using System;
using UnityEngine;

[Serializable]
public class UpgradeStats
{
    [SerializeField]
    UpgradeTarget target;          
    [SerializeField]
    PlayerUpgradeType playerType;  
    [SerializeField]
    WeaponUpgradeType weaponType;  

    [SerializeField]
    ModiferType modiferType;      
    [SerializeField]
    float value;

    [SerializeField]
    string displayName;            

    public UpgradeTarget Target
    {
        get => target;
        set => target = value;
    }

    public PlayerUpgradeType PlayerType
    {
        get => playerType;
        set => playerType = value;
    }

    public WeaponUpgradeType WeaponType
    {
        get => weaponType;
        set => weaponType = value;
    }

    public ModiferType ModiferType
    {
        get => modiferType;
        set => modiferType = value;
    }

    public float Value
    {
        get => value;
        set => value = value;
    }

    public string DisplayName
    {
        get => displayName;
        set => displayName = value;
    }
}
