using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private float spawnInterval = 5f;
    [SerializeField] private Transform[] spawnPoints;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), spawnInterval, spawnInterval);
    }
    private Vector3 GetAdjustedSpawnPosition(Transform spawnPoint)
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(spawnPoint.position, out hit, 10.0f, NavMesh.AllAreas))
        {
            return hit.position; 
        }
        else
        {
            return spawnPoint.position;
        }
    }
    private void SpawnEnemy()
    {
        if (spawnPoints.Length == 0)
        {
            return;
        }
        Transform selectedSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Vector3 adjustedPosition = GetAdjustedSpawnPosition(selectedSpawnPoint);

        GameObject enemy = ObjectPoolingManager.Instance.GetEnemyFromPool(adjustedPosition);
        if (enemy != null)
        {
            Debug.Log($"Enemy successfully spawned at {adjustedPosition}");
        }
        else
        {
            Debug.LogWarning($"Failed to spawn an enemy at {adjustedPosition}. Trying again...");
        }
    }
}
