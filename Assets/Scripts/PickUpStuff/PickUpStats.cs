using UnityEngine;

[System.Serializable]
public class PickUpStats
{
    
    [SerializeField]
    float amont;
    [SerializeField]
    PickUpEnum pickUpType;


    public float Amont { get => amont; set => amont = value; }
    public PickUpEnum PickUpType { get => pickUpType; set => pickUpType = value; }
}
