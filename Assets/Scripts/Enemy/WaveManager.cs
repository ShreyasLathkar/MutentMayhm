using System.Collections;
using UnityEngine;
using System;
public class WaveManager : MonoBehaviour
{
    public Wave[] waves; // Array of wave configurations
    public float timeBetweenWaves = 10f; // Time between waves
    public float spawnRadius = 10f; // Radius within which enemies can spawn
    public Transform spawnPoint; // Central point around which enemies will spawn

    private int currentWaveIndex = 0; // Index of the current wave

    public static Action<string> PlayerWon;
    private void Start()
    {
        // Start the first wave
        StartCoroutine(SpawnWave());
    }

    private IEnumerator SpawnWave()
    {
        // Wait for the inter-wave period
        yield return new WaitForSeconds(timeBetweenWaves);

        // Get the current wave configuration
        Wave currentWave = waves[currentWaveIndex];

        // Spawn enemies for the current wave
        for (int i = 0; i < currentWave.enemyCount; i++)
        {
            SpawnEnemy(currentWave.enemyPrefab);
            yield return new WaitForSeconds(currentWave.spawnInterval);
        }

        // Increment the wave index
        currentWaveIndex++;

        // Check if there are more waves remaining
        if (currentWaveIndex < waves.Length)
        {
            // Start the next wave
            StartCoroutine(SpawnWave());
        }
        else
        {
            // Check if all enemies are defeated
            while (GameObject.FindWithTag("Enemy") != null)
            {
                yield return null; // Wait until all enemies are defeated
            }

            // Trigger game over or victory condition
            PlayerWon?.Invoke("OK you Won!!");
            Debug.Log("All enemies defeated! Game Over or Victory!");

            // You can add your game over or victory logic here
        }
    }

    private void SpawnEnemy(GameObject enemyPrefab)
    {
        // Generate a random point within the spawn radius
        Vector3 randomPoint =UnityEngine.Random.insideUnitSphere * spawnRadius;
        randomPoint += spawnPoint.position; // Offset by the spawn point position

        // Ensure the enemy spawns at ground level
        randomPoint.y = spawnPoint.position.y;

        // Instantiate the enemy prefab at the random point
        Instantiate(enemyPrefab, randomPoint, Quaternion.identity);
    }
}