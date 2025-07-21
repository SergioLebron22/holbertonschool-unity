using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public int maxObstacles = 2;
    public Vector3 spawnAreaCenter;
    public Vector3 spawnAreaSize;
    public LayerMask obstacleLayer;
    public float spawnHeightOffset = 0.5f;

    private bool hasSpawned = false;

    public void SpawnObstacles()
    {
        if (hasSpawned) return;
        hasSpawned = true;

        int spawned = 0;
        int maxAttempts = 100;

        while (spawned < maxObstacles && maxAttempts > 0)
        {
            Vector3 randomPos = spawnAreaCenter + new Vector3(
                Random.Range(-spawnAreaSize.x / 2f, spawnAreaSize.x / 2f),
                0,
                Random.Range(-spawnAreaSize.z / 2f, spawnAreaSize.z / 2f)
            );

            Collider[] overlaps = Physics.OverlapBox(randomPos, Vector3.one * 0.5f, Quaternion.identity, obstacleLayer);
            if (overlaps.Length == 0)
            {
                Vector3 spawnPos = randomPos + Vector3.up * spawnHeightOffset; 
                Quaternion spawnRotation = Quaternion.Euler(90f, 0f, 0f);      
                Instantiate(obstaclePrefab, spawnPos, spawnRotation);

                spawned++;
            }

            maxAttempts--;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(spawnAreaCenter, spawnAreaSize);
    }
}
