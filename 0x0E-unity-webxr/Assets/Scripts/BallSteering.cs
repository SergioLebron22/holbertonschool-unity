using UnityEngine;

public class BallSteering : MonoBehaviour
{
    public float steeringForce = 0.05f;
    public float controlDuration = 10f;

    private Rigidbody rb;
    private float controlTimer = 0f;
    private bool isRolling = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void OnRelease()
    {
        isRolling = true;
        controlTimer = controlDuration;
    }

    void Update()
    {
        if (!isRolling || controlTimer <= 0f) return;

        float steerInput = Input.GetAxis("Horizontal"); 
        if (Mathf.Abs(steerInput) > 0.1f)
        {
            Vector3 right = Vector3.Cross(Vector3.up, rb.velocity.normalized);
            rb.AddForce(right * steerInput * steeringForce, ForceMode.Acceleration);
        }

        controlTimer -= Time.deltaTime;
    }
}
