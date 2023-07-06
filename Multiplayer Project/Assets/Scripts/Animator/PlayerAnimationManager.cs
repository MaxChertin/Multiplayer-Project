using System;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerMovement playerMovement;
    // Reference to input sys
    [SerializeField] private PlayerInput input;
    
    private float x, z;
    private bool isJumping;
    private bool isRunning;
    private bool isGrounded;

    // Animation refrences
    // Hashes
    private int MOVEMENT_INDEX => Animator.StringToHash("moveIndex");
    // Ints
    const int MOVEMENT_NO_MOVEMENT = 0; 
    const int MOVEMENT_WALK = 1; 
    const int MOVEMENT_RUN = 2;


    private void Update()
    {
        x = input.x;
        z = input.z;
        isJumping = input.isJumping;
        isRunning = input.isRunning;
        isGrounded = playerMovement.grounded;

        if ((x != 0 || z != 0) && isGrounded) {
            animator.SetInteger(MOVEMENT_WALK, isRunning ? MOVEMENT_RUN : MOVEMENT_WALK);
        }
        
        else if (x == 0 && z == 0)
            animator.SetInteger(MOVEMENT_INDEX, MOVEMENT_NO_MOVEMENT);
    }

    public void FootStepSoundLocalEvent()
    {
        return;
    }
    
}
