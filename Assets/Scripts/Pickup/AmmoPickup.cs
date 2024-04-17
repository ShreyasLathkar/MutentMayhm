using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public int ammoAmount = 30; // Amount of ammo to provide

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider is the player
        if (other.CompareTag("Player"))
        {
            // Get the ApexPredatorAssaultRifle component from the player object
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();

            // Check if the assaultRifle component is not null
            if (playerMovement != null)
            {
                // Increase ammo count in the assault rifle by the specified amount
                playerMovement.AddAmmo(ammoAmount);

                // Destroy the ammo pickup object
                Destroy(gameObject);
            }
        }
    }
}
