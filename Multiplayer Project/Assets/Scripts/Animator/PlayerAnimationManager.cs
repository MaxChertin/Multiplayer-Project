using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    [SerializeField] private Animator FP_animator;
    [SerializeField] private Animator CHARACTER_animator;
    [SerializeField] private PlayerMovement playerMovement;
    // Reference to input sys
    [SerializeField] private PlayerInput input;
    
    [Header("Values")]
    [SerializeField] [Range(0, 0.2f)] private float animationDampTime = .08f;
    
    private float x, z;
    private bool isJumping;
    private bool isRunning;
    private bool isGrounded;

    // Animation refrences
    // Hashes
    private int MOVEMENT_INDEX => Animator.StringToHash("moveIndex");
    private int NETPLAYER_MovX => Animator.StringToHash("MovX");
    private int NETPLAYER_MovZ => Animator.StringToHash("MovZ");
    private int NETPLAYER_JUMPING => Animator.StringToHash("isJumping");
    private int NETPLAYER_GROUNDED => Animator.StringToHash("isGrounded");
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
        
        LocalPlayerAnimate();
        NetPlayerAnimate();
    }

    private void LocalPlayerAnimate()
    {
        if ((x != 0 || z != 0) && isGrounded) {
            FP_animator.SetInteger(MOVEMENT_INDEX, isRunning ? MOVEMENT_RUN : MOVEMENT_WALK);
        }
        
        else if (x == 0 && z == 0)
        {
            FP_animator.SetInteger(MOVEMENT_INDEX, MOVEMENT_NO_MOVEMENT);
        }
    }

    private void NetPlayerAnimate()
    {
        if (isGrounded)
        {
            CHARACTER_animator.SetFloat(NETPLAYER_MovX, isRunning ? x * 2f : x, animationDampTime, Time.deltaTime);
            CHARACTER_animator.SetFloat(NETPLAYER_MovZ, isRunning ? z * 2f : z, animationDampTime, Time.deltaTime);
        }
        
        CHARACTER_animator.SetBool(NETPLAYER_JUMPING, isJumping);
        CHARACTER_animator.SetBool(NETPLAYER_GROUNDED, isGrounded);
    }

    public void FootStepSoundLocalEvent()
    {
        return;
    }
}
