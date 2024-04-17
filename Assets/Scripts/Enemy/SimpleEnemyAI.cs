using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyAI : MonoBehaviour
{
    public float moveSpeed = 3f; // Movement speed of the enemy
    public float rotationSpeed = 180f; // Rotation speed of the enemy
    public float detectionRange = 10f; // Range within which the enemy detects the player
    public float attackRange = 2f; // Range within which the enemy attacks the player
    public float roamRange = 5f; // Range within which the enemy roams when not following the player
    public float roamInterval = 10f; // Time interval between roams

    private Transform player; // Reference to the player's transform
    private Rigidbody rb; // Reference to the Rigidbody component
    private bool isPlayerInRange; // Flag indicating if the player is within detection range
    private Vector3 roamPoint; // Random point for the enemy to roam to
    private float roamTimer; // Timer to track the time since the last roam action

    void Start()
    {
        // Find the player object using a tag
        player = GameObject.FindGameObjectWithTag("Player").transform;
        // Get the Rigidbody component
        rb = GetComponent<Rigidbody>();
        // Freeze rotation of the Rigidbody to prevent the enemy from tipping over
        rb.freezeRotation = true;
        // Set initial roam point
        SetRandomRoamPoint();
        // Start the roam timer
        roamTimer = roamInterval;
    }

    void FixedUpdate()
    {
        // Calculate the distance between the enemy and the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Check if the player is within detection range
        if (distanceToPlayer <= detectionRange)
        {
            isPlayerInRange = true;
            // Rotate towards the player
            RotateTowardsPlayer();

            // If the player is within attack range, attack
            if (distanceToPlayer <= attackRange)
            {
                AttackPlayer();
            }
            else
            {
                // Move towards the player
                MoveTowardsPlayer();
            }
        }
        else
        {
            isPlayerInRange = false;
            // Update the roam timer
            roamTimer -= Time.deltaTime;
            // Roam when the player is not in range and the roam timer reaches zero
            if (roamTimer <= 0f)
            {
                Roam();
                // Reset the roam timer
                roamTimer = roamInterval;
            }
        }
    }

    void MoveTowardsPlayer()
    {
        // Calculate the direction towards the player
        Vector3 direction = (player.position - transform.position).normalized;
        // Move the enemy towards the player using Rigidbody
        rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
    }

    void RotateTowardsPlayer()
    {
        // Calculate the direction towards the player
        Vector3 direction = (player.position - transform.position).normalized;
        // Calculate the rotation towards the player
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        // Rotate the enemy towards the player gradually using Rigidbody
        rb.MoveRotation(Quaternion.RotateTowards(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime));
    }

    void AttackPlayer()
    {
        // Perform attack action (e.g., reduce player's health)
        Debug.Log("Attacking player!");
    }

    void Roam()
    {
        // Set a new random roam point
        SetRandomRoamPoint();
        // Calculate the direction towards the roam point
        Vector3 direction = (roamPoint - transform.position).normalized;
        // Move the enemy towards the roam point using Rigidbody
        rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
        // Rotate the enemy continuously while roaming
        RotateContinuously();
    }

    void SetRandomRoamPoint()
    {
        // Generate a random point within the roam range
        roamPoint = transform.position + Random.insideUnitSphere * roamRange;
        roamPoint.y = transform.position.y; // Ensure the roam point stays at the same height as the enemy
    }

    void RotateContinuously()
    {
        // Rotate the enemy continuously around its up axis
        rb.MoveRotation(rb.rotation * Quaternion.Euler(0f, rotationSpeed * Time.deltaTime, 0f));
    }

    void OnDrawGizmosSelected()
    {
        // Draw a wire sphere around the enemy to visualize the detection range in the Unity Editor
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        // Draw a wire sphere around the enemy to visualize the attack range in the Unity Editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        // Draw a wire sphere around the enemy to visualize the roam range in the Unity Editor
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, roamRange);
    }
}
