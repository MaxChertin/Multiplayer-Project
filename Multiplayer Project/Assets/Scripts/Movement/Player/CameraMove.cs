using Mirror;
using UnityEngine;

public class CameraMove : NetworkBehaviour
{
    [SerializeField] private Transform player;

    [Header("Mouse Settings")] [SerializeField]
    private float sens = 0.8f;

    private float xRotation;
    private float yRotation;

    private void Start()
    {
        if (!isLocalPlayer)
        {
            Destroy(this.gameObject);
            return;
        }
        
        // Temporary code for locking the cursor to the screen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    void Update()
    {
        // Move the camera to the player camera bone to avoid jitteriness
        // transform.position = cameraBone.transform.position;
        
        // Look and rotate the camera with the cursor
        float mouseX = Input.GetAxisRaw("Mouse X") * sens;
        float mouseY = Input.GetAxisRaw("Mouse Y") * sens;

        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        player.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
