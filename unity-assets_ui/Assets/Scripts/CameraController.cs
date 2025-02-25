using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public Vector3 offset;  
    public float rotationSpeed = 3f; 
    private float yaw = 0f;   
    private float pitch = 20f; 

    public bool isInverted;

    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        if (player != null)
        {
            if (Input.GetMouseButton(1))
            {
                yaw += Input.GetAxis("Mouse X") * rotationSpeed;
                
                if (isInverted) {
                    pitch += Input.GetAxis("Mouse Y") * rotationSpeed;
                } else {
                    pitch -= Input.GetAxis("Mouse Y") * rotationSpeed;
                }

                pitch = Mathf.Clamp(pitch, 5f, 60f);
            }

            Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
            transform.position = player.transform.position + rotation * offset;
            
            transform.LookAt(player.transform.position);
        }
    }
}
