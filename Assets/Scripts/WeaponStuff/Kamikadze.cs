using UnityEngine;

public class Kamikadze : Weapon
{
    bool hasExploded = false;
    [SerializeField]
    float explosionRadius;

    public override bool canAttack()
    {
        return !hasExploded;
    }

    public override void attack()
    {
        if (canAttack())
        {
            Explode();
            hasExploded = true;
            Debug.Log(damage);
        }
        else
        {
            Debug.Log("Kamikadze has already exploded!");
        }
    }

    public void Explode()
    {
        Debug.Log("Kamikadze exploded!");
        Debug.Log($"Explosion radius: {explosionRadius}");
    }

}
