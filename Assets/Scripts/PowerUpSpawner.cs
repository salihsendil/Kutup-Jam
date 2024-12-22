using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private GameObject powerUpPrefab; 
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private float spawnInterval = 10f; 
    [SerializeField] private int maxActivePowerUps = 3; 

    private int activePowerUps = 0; 

    private void Start()
    {
        InvokeRepeating(nameof(SpawnPowerUp), spawnInterval, spawnInterval);
    }

    private void SpawnPowerUp()
    {
        if (activePowerUps >= maxActivePowerUps)
        {
            Debug.Log("Maximum Power-Ups active, skipping spawn.");
            return;
        }

        if (spawnPoints.Length == 0)
        {
            Debug.LogWarning("No spawn points available.");
            return;
        }

        Transform selectedSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject newPowerUp = Instantiate(powerUpPrefab, selectedSpawnPoint.position, Quaternion.identity);
        
        HealPowerUp powerUpComponent = newPowerUp.GetComponent<HealPowerUp>();
        if (powerUpComponent != null && powerUpComponent.IsInteractable)
        {
            activePowerUps++;
        }

        
    }
}
