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

    private AudioSource footstepAudio;
    private AudioSource landingAudio;
    public AudioClip grassFootsteps;
    public AudioClip stoneFootsteps;
    public AudioClip grassLanding;
    public AudioClip stoneLanding;

    private string currentSurfaceTag = "Grass";
    void Start()
    {
        controller = GetComponent<CharacterController>();
        footstepAudio = GetComponents<AudioSource>()[0];
        landingAudio = GetComponents<AudioSource>()[1];
    }

    void Update()
    {
        isGrounded = controller.isGrounded;

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

        if (isGrounded && moveDirection != Vector3.zero) {
            Quaternion wantedRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, wantedRotation, rotationSpeed * Time.deltaTime);
        }
        else {
            if (footstepAudio.isPlaying) {
                footstepAudio.Stop();
            }
        }

        animator.SetFloat("Speed", moveDirection.magnitude * speed);

        // Jump
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * 2f * gravity);
            animator.SetTrigger("Jump");
        }

    }

    public void PlayFootstepSounds() {
        Debug.Log("Footstep sound played");
        if (!footstepAudio.isPlaying) {
            footstepAudio.clip = currentSurfaceTag == "Grass" ? grassFootsteps : stoneFootsteps;
            footstepAudio.loop = true;
            footstepAudio.Play();
        }
    }

    public void PlayLandingSound() {
        Debug.Log("Landed fall flat!");
        if (isGrounded && !landingAudio.isPlaying) {
            landingAudio.clip = currentSurfaceTag == "Grass" ? grassLanding : stoneLanding;
            landingAudio.Play();
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit) {
        Debug.Log("Colliding!");
        
        if (hit.gameObject.CompareTag("Grass")) {
            currentSurfaceTag = "Grass";
        }
        else if (hit.gameObject.CompareTag("Stone")) {
            currentSurfaceTag = "Stone";
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
