using UnityEngine;

public class SimpleNonVRPlayerController : MonoBehaviour
{
    public float speed = 2f;
    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float moveZ = Input.GetAxis("Vertical"); 
        Vector3 move = transform.forward * moveZ;
        controller.Move(move * speed * Time.deltaTime);
    }
}
