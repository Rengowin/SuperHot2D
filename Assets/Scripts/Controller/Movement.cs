using UnityEngine; 
using UnityEngine.InputSystem; 
public class Movement : MonoBehaviour 
{ 
    [Header("Movement Settings")] public float moveForce = 20f;
    public float maxSpeed = 6f; public float resistance = 5f; 
    [Header("Impulse Settings")] public float impulseStrength = 10f; 
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
        physics.ApplyMovement(input); physics.ApplyResistance(input); 
        physics.CapSpeed(); 
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
}