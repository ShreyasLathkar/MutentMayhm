using UnityEngine;

[System.Serializable]
public class Wave
{
    public GameObject enemyPrefab; // Prefab of the enemy to spawn
    public int enemyCount; // Number of enemies to spawn in this wave
    public float spawnInterval; // Time between each enemy spawn
}