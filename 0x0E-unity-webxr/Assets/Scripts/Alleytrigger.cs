using UnityEngine;

public class AlleyTrigger : MonoBehaviour
{
    private bool hasSpawned = false;

    void OnTriggerEnter(Collider other)
    {
        if (!hasSpawned && other.CompareTag("Interactable")) 
        {
            FindObjectOfType<ObstacleSpawner>().SpawnObstacles();
            hasSpawned = true;
        }
    }
}
