using UnityEngine;

public abstract class Magic: Weapon
{
    [SerializeField]
    int manaCost;

    public abstract void Cast();
}
