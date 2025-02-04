using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public Vector3 offset;  // Distance between camera and player
    public float rotationSpeed = 3f; // Sensitivity of rotation
    private float yaw = 0f;   // Horizontal rotation (left/right)
    private float pitch = 20f; // Vertical rotation (up/down)

    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        if (player != null)
        {
            // Rotate camera when holding the right mouse button
            if (Input.GetMouseButton(1))
            {
                yaw += Input.GetAxis("Mouse X") * rotationSpeed;
                pitch -= Input.GetAxis("Mouse Y") * rotationSpeed;

                // Clamp pitch to prevent extreme angles
                pitch = Mathf.Clamp(pitch, 5f, 60f);
            }

            // Apply rotation
            Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
            transform.position = player.transform.position + rotation * offset;
            
            // Make camera look at the player
            transform.LookAt(player.transform.position);
        }
    }
}
