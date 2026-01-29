using UnityEngine; 
using UnityEngine.InputSystem; 
public class Movement : MonoBehaviour 
{ 
    [Header("Movement Settings")] public float moveForce = 20f;
    public float maxSpeed = 6f; public float resistance = 5f; 
    [Header("Impulse Settings")] public float impulseStrength = 10f; 
    [SerializeField] private Vector2 xBounds = new Vector2(-55f, 66f);
    [SerializeField] private Vector2 zBounds = new Vector2(-100f, 50f);
    [SerializeField] private PlayerVisualController visual;
    private PhysicsController physics; private Rigidbody rb; 

    private Vector2 input; void Awake() 

    { 
        rb = GetComponent<Rigidbody>(); 
        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation; 
        rb.useGravity = true; rb.isKinematic = false; physics = new PhysicsController(rb, moveForce, maxSpeed, resistance, impulseStrength);
        if (visual == null)
            visual = GetComponentInChildren<PlayerVisualController>();
 
    } 

    void Update() 
    { input = Vector2.zero; 
        if (Keyboard.current != null) 
        {
            if (Keyboard.current.aKey.isPressed) input.x -= 1; 
            if (Keyboard.current.dKey.isPressed) input.x += 1; 
            if (Keyboard.current.wKey.isPressed) input.y += 1; 
            if (Keyboard.current.sKey.isPressed) input.y -= 1; 
        } 
    } 

    void FixedUpdate()
    { 
        physics.ApplyMovement(input); 
        physics.ApplyResistance(input); 
        physics.CapSpeed(); 
        ClampPositionAndStop();
        //Debug.Log($"Input: {input}, Velocity: {rb.linearVelocity}");
        visual.UpdateFacing(rb.linearVelocity);
    } 

    public void TriggerImpulse(Vector3 direction) 
    { 
        physics.ApplyImpulse(direction); 
    } 

    public float GetResistance() 
    { 
        return physics.GetResistance(); 
    } 

    void ClampPositionAndStop()
{
    Vector3 pos = rb.position;
    Vector3 vel = rb.linearVelocity;

    // --- X bounds (hard stop) ---
    if (pos.x <= xBounds.x)
    {
        pos.x = xBounds.x;
        vel.x = 0f;
    }
    else if (pos.x >= xBounds.y)
    {
        pos.x = xBounds.y;
        vel.x = 0f;
    }

    // --- Z bounds (hard stop) ---
    if (pos.z <= zBounds.x)
    {
        pos.z = zBounds.x;
        vel.z = 0f;
    }
    else if (pos.z >= zBounds.y)
    {
        pos.z = zBounds.y;
        vel.z = 0f;
    }

    rb.position = pos;
    rb.linearVelocity = vel;
}
}