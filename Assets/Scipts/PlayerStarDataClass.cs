using UnityEngine;

[System.Serializable]
public class PlayerStarDataClass
{
    [Header ("Player Base Stats")]
    [SerializeField] int hitPoints; //Maybe float? or we work with dmg and so only in no float/double values
    [SerializeField] float movementSpeed;
    [SerializeField] float acceleration;


    public int HP
    {
        get => hitPoints;
        set => hitPoints = value;
    }

    public float MovementSpeed
    {
        get => movementSpeed;
        set => movementSpeed = value;
    }

    public float Acceleration
    {
        get => acceleration;
        set => acceleration = value;
    }

}
