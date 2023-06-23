using Mirror;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    [Header("References")]
    [SerializeField] private CharacterController controller;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask whatIsGround;
    [Header("Settings")]
    [SerializeField] [Range(0, 20f)] private float speed = 4.5f;
    [SerializeField] [Range(-9.81f, -39.24f)] private float gravity = -29.43f;
    [SerializeField] private float groundDistance = 0.3f;

    private Vector3 velocity;
    private float x, z;
    private bool grounded;
    
    private void Update()
    {
        if (!isLocalPlayer) return;
        
        // Input
        ReadInput();

        // Jumping
        grounded = Physics.CheckSphere(groundCheck.position, groundDistance, whatIsGround);
        if (grounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        if (Input.GetButton("Jump") && grounded)
        {
            velocity.y = Mathf.Sqrt(-2f * gravity);
        }
        
        // Basic movement
        Vector3 moveDir = transform.right * x + transform.forward * z;
        controller.Move(moveDir * speed * Time.deltaTime);
        
        // Gravity movement
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void ReadInput()
    {
        if (grounded)
        {
            x = Input.GetAxisRaw("Horizontal");
            z = Input.GetAxisRaw("Vertical");
        }
    }
}
