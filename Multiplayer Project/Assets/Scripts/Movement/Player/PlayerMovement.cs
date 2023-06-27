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

    private Vector3 velocity;
    private float x, z;
    private bool isJumping;
    private bool grounded;

    // Based on our input and our previous state, calculate the new state
    public ClientPrediction.GameState CalculateMovement(ClientPrediction.GameState previousState, ClientPrediction.PlayerInput playerInput)
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
            position = controller.transform.position// + previousState.position,
        };
    }

    private void Update()
    {
        if (!isLocalPlayer) return;
        
        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");
        isJumping = Input.GetButton("Jump");
        
        CalculateMovement();
    }

    private void CalculateMovement()
    {
        // Jumping
        grounded = Physics.CheckSphere(groundCheck.position, groundDistance, whatIsGround);
        if (grounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        if (isJumping && grounded)
        {
            velocity.y = Mathf.Sqrt(-2f * gravity);
        }
        
        // Basic movement
        Vector3 moveDir = transform.right * x + transform.forward * z;
        controller.Move(Time.deltaTime * speed * moveDir);
        
        // Gravity movement
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
