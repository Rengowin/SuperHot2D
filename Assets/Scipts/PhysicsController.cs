using UnityEngine;

public class PhysicsController
{
    private Rigidbody rb;

    private float moveForce;
    private float maxSpeed;
    private float resistance;
    private float impulseStrength;

    public PhysicsController(Rigidbody rb, float moveForce, float maxSpeed, float resistance, float impulseStrength)
    {
        this.rb = rb;
        this.moveForce = moveForce;
        this.maxSpeed = maxSpeed;
        this.resistance = resistance;
        this.impulseStrength = impulseStrength;
    }

    public float GetResistance()
    {
        return resistance;
    }

    public void ApplyMovement(Vector2 input)
    {
        if (input.sqrMagnitude > 0.01f)
        {
            Vector3 direction = new Vector3(input.x, 0f, input.y).normalized;
            rb.AddForce(direction * moveForce, ForceMode.Force);
        }
    }
    // impulse can be used for like a charge / dash attack of enemies or even a dashing movement of the player when a certain input is pressed
    public void ApplyImpulse(Vector3 direction)
    {
        rb.AddForce(direction.normalized * impulseStrength, ForceMode.Impulse);
    }

    //slows down movement depending on the resistance, can add diferent resistances value to object like bushes or different terrain to slow player movement or accelerate
    public void ApplyResistance(Vector2 input)
    {
        if (input.sqrMagnitude < 0.01f)
        {
            Vector3 resist = -rb.linearVelocity * resistance * Time.fixedDeltaTime;
            rb.AddForce(resist, ForceMode.VelocityChange);
        }
    }

    public void CapSpeed()
    {
        Vector3 v = rb.linearVelocity;

        // enforce Y lock through constraints — do NOT override position here
        v.y = 0f;

        if (v.magnitude > maxSpeed)
            rb.linearVelocity = v.normalized * maxSpeed;
        else
            rb.linearVelocity = v;
    }
}
