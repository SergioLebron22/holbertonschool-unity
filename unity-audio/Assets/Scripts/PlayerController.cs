using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 6f;
    private float rotationSpeed = 10f;
    public float jumpHeight = 3f;
    public float gravity = 20f;
    public Camera followCamera;
    public Vector3 respawnPosition = new Vector3(0, 50, 0);
    public float fallThreshold = -15f;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    public Animator animator;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        isGrounded = controller.isGrounded;

        Debug.Log($"isGrounded: {isGrounded}, velocity.y: {velocity.y}, IsFalling: {!isGrounded && velocity.y < -17f}");

        animator.SetBool("isGrounded", isGrounded);
        animator.SetBool("IsFalling", !isGrounded && velocity.y < -17f); 

        MovePlayer();
        ApplyGravity(); 

        if (transform.position.y <= fallThreshold)
        {
            Respawn();
        }
    }

    void MovePlayer()
    {
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0) 
        {
            velocity.y = -3f;
        }

        float HorizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementInput = Quaternion.Euler(0, followCamera.transform.eulerAngles.y, 0) * new Vector3(HorizontalInput, 0, verticalInput);
        Vector3 moveDirection = movementInput.normalized;

        controller.Move(moveDirection * speed * Time.deltaTime);

        if (moveDirection != Vector3.zero) {
            Quaternion wantedRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, wantedRotation, rotationSpeed * Time.deltaTime);
        }

        animator.SetFloat("Speed", moveDirection.magnitude * speed);

        // Jump
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * 2f * gravity);
            animator.SetTrigger("Jump");
        }

    }

    void ApplyGravity()
    {
        if (isGrounded)
        {
            if (velocity.y < 0)
            {
                velocity.y = -3f;
            }
        }
        else
        {
            velocity.y -= gravity * Time.deltaTime;
        }

        controller.Move(velocity * Time.deltaTime);
    }

    void Respawn()
    {
        transform.position = respawnPosition;
        velocity = Vector3.zero; // Reset velocity when respawning
    }

    bool CheckIfGrounded()
    {
        float groundCheckDistance = 0.2f; // Distance to check for ground
        Vector3 origin = transform.position + Vector3.up * 0.1f; // Slightly above the player's position
        return Physics.Raycast(origin, Vector3.down, groundCheckDistance);
    }
}
