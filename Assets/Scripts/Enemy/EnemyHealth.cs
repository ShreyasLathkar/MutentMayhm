using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f; // Maximum health of the enemy
    public GameObject ammoPrefab; // Prefab for armor pickup
    public GameObject healthPrefab; // Prefab for health pickup
    public float dropChance = 0.5f; // Chance of dropping an item (0 to 1)
    private float currentHealth; // Current health of the enemy

    void Start()
    {
        currentHealth = maxHealth; // Set initial health to max health
    }

    // Method to take damage
    public void TakeDamage(float damage)
    {
        currentHealth -= damage; // Reduce current health by the damage amount

        if (currentHealth <= 0)
        {
            Die(); // If health drops to or below zero, call the Die method
        }
    }

    // Method to handle enemy death
    void Die()
    {
        // Perform death-related actions here (e.g., play death animation, deactivate object, etc.)

        // Randomly determine if the enemy drops an item
        if (Random.value <= dropChance)
        {
            DropItem(); // Call the method to drop an item
        }

        Destroy(gameObject); // Destroy the enemy object
    }

    // Method to drop an item (armor, health, or nothing)
    void DropItem()
    {
        float randomAmmoValue = Random.value; // Generate a random value

        if (randomAmmoValue < 0.5f) // 33% chance of dropping armor
        {
            Instantiate(ammoPrefab, transform.position, Quaternion.identity);
        }
        else if (randomAmmoValue < 0.8f) // 33% chance of dropping health
        {
            Instantiate(healthPrefab, transform.position, Quaternion.identity);
        }
        // Otherwise, 33% chance of dropping nothing (no action required)
    }
}
