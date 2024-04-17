using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float damage = 20f; // Damage per bullet
    private float speed = 50f; // Speed of the bullet
    private float range = 100f; // Maximum range of the bullet
    private Vector3 startPosition; // Initial position of the bullet

    void Start()
    {
        // Store the initial position of the bullet
        startPosition = transform.position;
    }

    void Update()
    {
        // Move the bullet forward
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // Check if the bullet has traveled beyond its range
        if (Vector3.Distance(startPosition, transform.position) >= range)
        {
            // Destroy the bullet if it's out of range
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the bullet collides with an enemy
        EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            // Deal damage to the enemy
            enemyHealth.TakeDamage(damage);
        }

        // Destroy the bullet upon collision with any object
        Destroy(gameObject);
    }

    // Method to set the damage of the bullet
    public void SetDamage(float newDamage)
    {
        damage = newDamage;
    }

    // Method to set the range of the bullet
    public void SetRange(float newRange)
    {
        range = newRange;
    }
}
