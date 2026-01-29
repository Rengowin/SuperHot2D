using UnityEngine;

public class DamageOnTouch : MonoBehaviour
{
    [SerializeField] private float contactDamage = 10f;

    public float ContactDamage => contactDamage;
}
