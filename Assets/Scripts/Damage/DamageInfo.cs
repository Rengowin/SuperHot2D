using UnityEngine;

[System.Serializable]
public struct DamageInfo
{
    public float damageAmount;
    public DamageSource source;
    public WeaponType weaponType;
    public GameObject attacker;
}
