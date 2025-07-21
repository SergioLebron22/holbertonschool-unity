using UnityEngine;

public class ObstacleDestroyBall : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Interactable"))
        {
            Destroy(collision.gameObject);
        }
    }
}
