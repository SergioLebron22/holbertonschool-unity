using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    public float boostForce = 10f; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 boostDirection = rb.velocity.normalized;
                rb.AddForce(boostDirection * boostForce, ForceMode.VelocityChange);
            }
        }
    }
}
