using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healthAmount = 1; // Amount of health to restore

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider is the player
        if (other.CompareTag("Player"))
        {
            // Get the PlayerMovement component from the player object
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();

            // Check if the playerMovement component is not null
            if (playerMovement != null)
            {
                // Increase player health by the specified amount
                playerMovement.RestoreHealth(healthAmount);

                // Destroy the health pickup object
                Destroy(gameObject);
            }
        }
    }
}
