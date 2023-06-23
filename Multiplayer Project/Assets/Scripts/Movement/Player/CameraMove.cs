using Mirror;
using UnityEngine;

public class CameraMove : NetworkBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform head;

    [Header("Mouse Settings")] [SerializeField]
    private float sens = 300f;

    private float xRotation;
    private float yRotation;

    private void Start()
    {
        if (!isLocalPlayer) Destroy(this.gameObject);
        
        // Temporary code for locking the cursor to the screen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    void Update() {
        // Move the camera to the player
        transform.position = player.transform.position + new Vector3(0, head.transform.localPosition.y, 0);
        
        // Look and rotate the camera with the cursor
        float mouseX = Input.GetAxisRaw("Mouse X") * sens * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * sens * Time.deltaTime;

        yRotation += mouseX;
        xRotation -= mouseY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        player.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
