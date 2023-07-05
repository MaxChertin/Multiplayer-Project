using System;
using Mirror;
using UnityEngine;

public class PlayerInput : NetworkBehaviour
{
    public static PlayerInput Instance;

    public float x;
    public float z;
    public bool isJumping;
    public bool isRunning;
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(this);
    }

    private void Update()
    {
        if (!isLocalPlayer) return;
        
        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");
        isRunning = Input.GetKey(KeyCode.LeftShift);
        isJumping = Input.GetButton("Jump");
    }
}
