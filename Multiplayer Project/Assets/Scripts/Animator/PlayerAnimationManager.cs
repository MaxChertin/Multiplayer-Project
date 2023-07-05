using System;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    public static PlayerAnimationManager Instance;
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerInput input;
    
    private float x, z;
    private bool isJumping;
    private bool isRunning;
    private bool isGrounded;

    // Animation strings
    // Strings
    const string MOVEMENT_INDEX = "moveIndex";
    // Ints
    const int MOVEMENT_NO_MOVEMENT = 0; 
    const int MOVEMENT_WALK = 1; 
    const int MOVEMENT_RUN = 2;

    // Reference to input sys

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(this);
    }

    private void Update()
    {
        x = input.x;
        z = input.z;
        isJumping = input.isJumping;
        isRunning = input.isRunning;
        isGrounded = playerMovement.grounded;

        if ((x != 0 || z != 0) && isGrounded)
        {
            if (isRunning)
            {
                animator.SetInteger(MOVEMENT_INDEX, MOVEMENT_RUN);
            }
            
            else animator.SetInteger(MOVEMENT_INDEX, MOVEMENT_WALK);
        }
        
        else if (x == 0 && z == 0)
            animator.SetInteger(MOVEMENT_INDEX, MOVEMENT_NO_MOVEMENT);
    }

    public void FootStepSoundLocalEvent()
    {
        return;
    }
    
}
