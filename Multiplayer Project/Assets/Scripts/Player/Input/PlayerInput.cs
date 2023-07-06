using System;
using Mirror;
using UnityEngine;

public class PlayerInput : NetworkBehaviour
{
    public float x;
    public float z;
    public bool isJumping;
    public bool isRunning;

    private void Update()
    {
        if (!isLocalPlayer) return;

        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");
        isRunning = Input.GetKey(KeyCode.LeftShift);
        isJumping = Input.GetButton("Jump");
    }
}
