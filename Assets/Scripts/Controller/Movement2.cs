using UnityEngine;
using UnityEngine.InputSystem;

public class Movement2 : MonoBehaviour
{
    [Header("Movement")]
    public float acceleration = 20f;    
    public float maxSpeed = 6f;         
    public float damping = 5f;          // How fast sliding slows down

    private Vector3 velocity;           
    private Vector2 input;          

    void Update()
    {
        input = Vector2.zero;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.aKey.isPressed) input.x -= 1;
            if (Keyboard.current.dKey.isPressed) input.x += 1;
            if (Keyboard.current.wKey.isPressed) input.y += 1;
            if (Keyboard.current.sKey.isPressed) input.y -= 1;
        }

        Vector3 desiredDirection = new Vector3(input.x, 0f, input.y).normalized;

        if (desiredDirection.magnitude > 0)
        {
            velocity += desiredDirection * acceleration * Time.deltaTime;
        }
        else
        {
            velocity = Vector3.Lerp(velocity, Vector3.zero, damping * Time.deltaTime);
        }

        if (velocity.magnitude > maxSpeed)
            velocity = velocity.normalized * maxSpeed;

        // --- Apply movement ---
        transform.position += velocity * Time.deltaTime;

        Vector3 pos = transform.position;
        pos.y = 0f;
        transform.position = pos;
    }
}
