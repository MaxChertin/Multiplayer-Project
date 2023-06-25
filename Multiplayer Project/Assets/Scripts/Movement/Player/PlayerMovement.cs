using System;
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

    [SerializeField] private UIManager ui;

    private Vector3 velocity;
    private float x, z;
    private bool grounded;
    
    private void FixedUpdate()
    {
        if (!isLocalPlayer) return;
        
        // Input
        // Vector2 input = ReadInput();
        //CmdMoveOnServer(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), Input.GetButton("Jump"));

        /*    // Jumping
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
            controller.Move(Time.deltaTime * speed * moveDir);
            
            // Gravity movement
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);  */
    }
    
    private void Update()
    {
        //Debug.Log(this.gameObject.transform.position);
        
        //if (!isLocalPlayer) return;
        //Movement(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), Input.GetButton("Jump"));
    }

    //[Command]
    //private void CmdMoveOnServer(float x, float y, bool jump)
    //{
    //    Movement(x, y, jump);
    //}
    
    // Testing
    private void Movement(/*ClientPrediction.PlayerState previousState, */float x, float z, bool jump)
    {
        Debug.Log("In Movement");
        // Jumping
        grounded = Physics.CheckSphere(groundCheck.position, groundDistance, whatIsGround);
        if (grounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        if (jump && grounded)
        {
            velocity.y = Mathf.Sqrt(-2f * gravity);
        }
        
        // Basic movement
        Vector3 moveDir = transform.right * x + transform.forward * z;
        controller.Move(Time.deltaTime * speed * moveDir);
        
        // Gravity movement
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        /*return new ClientPrediction.PlayerState
        {
            timestamp = previousState.timestamp + 1,
            position = controller.transform.position,
        };*/
    }
    
    public ClientPrediction.GameState CalculateMovement(ClientPrediction.PlayerInput playerInput)
    {
        float x = playerInput.input.x, z = playerInput.input.y;
        bool jump = playerInput.isJumping;
        
        // Jumping
        grounded = Physics.CheckSphere(groundCheck.position, groundDistance, whatIsGround);
        if (grounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        if (jump && grounded)
        {
            velocity.y = Mathf.Sqrt(-2f * gravity);
        }
        
        // Basic movement
        Vector3 moveDir = transform.right * x + transform.forward * z;
        controller.Move(Time.deltaTime * speed * moveDir);
        
        // Gravity movement
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        return new ClientPrediction.GameState
        {
            usedInput = playerInput,
            position = controller.transform.position,
        };
    }
}
