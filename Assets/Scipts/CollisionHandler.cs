using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision) //Collision Check
    {
        GameObject other = collision.gameObject;

        if (other.CompareTag("Ground"))
        {
            OnGroundHit(collision);
        }
        else if (other.CompareTag("Wall"))
        {
            OnWallHit(collision);
        }
        else if (other.CompareTag("Enemy"))
        {
            OnEnemyHit(collision);
        }
        else if (other.CompareTag("Projectile"))
        {
            OnProjectileHit(collision);
        }
        else if (other.CompareTag("Hazard"))
        {
            OnHazardHit(collision);
        }
        else if (other.CompareTag("Platform"))
        {
            OnPlatformHit(collision);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pickup"))
        {
            OnPickup(other);
        }
        else if (other.CompareTag("Checkpoint"))
        {
            OnCheckpoint(other);
        }
    }

    //Collision Response

    private void OnGroundHit(Collision collision)
    {
        Debug.Log("Landed on ground");
    }

    private void OnWallHit(Collision collision)
    {
        Debug.Log("Hit wall");
    }

    private void OnEnemyHit(Collision collision)
    {
        Debug.Log("Hit enemy");
    }

    private void OnProjectileHit(Collision collision)
    {
        Debug.Log("Projectile Hit");
    }
    private void OnHazardHit(Collision collision)
    {
        Debug.Log("Hit hazard");
    }

    private void OnPlatformHit(Collision collision)
    {
        Debug.Log("On platform");
    }

    private void OnPickup(Collider other)
    {
        Debug.Log("Picked up item");
        Destroy(other.gameObject);
    }

    private void OnCheckpoint(Collider other)
    {
        Debug.Log("Checkpoint reached");
    }
}
