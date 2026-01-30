using UnityEngine;

public class ExplodeOnTouch : MonoBehaviour
{
    [SerializeField] private float contactDamage = 10f;

    public float ContactDamage => contactDamage;
}
