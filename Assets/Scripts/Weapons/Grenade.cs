using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float explosionRadius = 5f; // Radius of the explosion
    public float explosionForce = 5f; // Force of the explosion
    public float detonationTime = 3f; // Time before the grenade detonates
    public GameObject explosionEffectPrefab; // Prefab of the explosion effect
    public float explosionDamage = 50f; // Damage caused by the explosion

    private Rigidbody rb; // Reference to the grenade's Rigidbody component
    private bool hasDetonated = false; // Flag to track if the grenade has detonated

    void Start()
    {

        // Start the detonation timer
        Invoke("Detonate", detonationTime);

        // Get reference to the Rigidbody component
        rb = GetComponent<Rigidbody>();

        // Apply initial force to the grenade
       // rb.AddForce(transform.forward * explosionForce, ForceMode.Impulse);
    }

    void Detonate()
    {
        if (!hasDetonated)
        {
            // Spawn explosion effect
            Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);

            // Find all colliders within the explosion radius
            Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

            foreach (Collider nearbyObject in colliders)
            {
                // Apply explosion force to Rigidbody components
                Rigidbody nearbyRb = nearbyObject.GetComponent<Rigidbody>();
                if (nearbyRb != null)
                {
                    nearbyRb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
                }

                // Apply damage to objects with health components
             /*   Health health = nearbyObject.GetComponent<Health>();
                if (health != null)
                {
                    health.TakeDamage(explosionDamage);
                }*/
            }

            // Destroy the grenade
            Destroy(gameObject);

            hasDetonated = true;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Detonate the grenade upon collision with any object
        Detonate();
    }
}
