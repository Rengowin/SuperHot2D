using UnityEngine;

[System.Serializable]
public static class MakeLoadOut 
{ 
    public static Weapon CreateWeapon(WeaponsEnum weaponType, WeaponStats stats)
    {
        switch (weaponType)
        {
            case WeaponsEnum.Pistol:
                return new Pistol(stats);
            case WeaponsEnum.ShotGun:
                return new ShotGun(stats);
            case WeaponsEnum.RocketLauncher:
                return new RocketLauncher(stats);
            case WeaponsEnum.Swords:
                return new Swords(stats);
            case WeaponsEnum.Spear:
                return new Spear(stats);
            default:
                Debug.LogError("Invalid weapon type");
                return null;
        }
    }




}
