using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlayerMovement : MonoBehaviour
{


    float playerSpeed = 5f; // Movement speed
    public float jumpForce = 5f; // Jump force
    public float mouseSensitivity = 100f; // Mouse sensitivity
    public Transform playerBody; // Reference to the player's body
    public Transform playerCamera; // Reference to the player's camera
    public LayerMask groundLayer; // Layer mask for detecting ground
    public GameObject gameOverPanel; // Reference to the game over panel
    public TextMeshProUGUI HealthUI;
    public GameObject[] hearts; // Array of heart objects representing player health
    private Rigidbody rb; // Reference to the Rigidbody component
    private float groundCheckRadius = .1f; // Radius of the spherecast for ground check
    private bool isGameOver = false; // Flag to track if game over condition is reached
    [SerializeField] private int maxPlayerHealth = 5; // Maximum player health (number of hearts)
    private int playerHealth; // Current player health
    private float xRotation = 0f; // Current rotation around the X-axis
    private Vector3 startPosition; // Initial position of the player
    public float maxRadius = 10f; // Maximum radius from the starting position
    float moveHorizontal;
    float moveVertical;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float sprintSpeed;
    bool jumpReq;


    public static Action<string> PlayerDied;
    private void Start()
    {
        // Initialize player health
        playerHealth = maxPlayerHealth;
        UpdateHealthUI();

        // Lock the cursor to the center of the screen and hide it
        Cursor.lockState = CursorLockMode.Locked;
        // Get the reference to the Rigidbody component attached to the player
        rb = GetComponent<Rigidbody>();
        // Freeze rotation of the Rigidbody so it doesn't topple over
        rb.freezeRotation = true;

        // Store the initial position of the player
        startPosition = transform.position;
    }

    private void Update()
    {
        if (!isGameOver) // Check if game over condition is not reached
        {
            if (Input.GetButtonDown("Jump") && IsGrounded())
                jumpReq = true;
            // Check for player input and perform actions accordingly
            // HandleMovementInput();

            // Get player movement input
            moveHorizontal = Input.GetAxis("Horizontal");
            moveVertical = Input.GetAxis("Vertical");

            if (Input.GetKey(KeyCode.LeftShift))
                playerSpeed = sprintSpeed;
            else playerSpeed = moveSpeed;
        }
    }

    private void FixedUpdate()
    {
        if (!isGameOver) // Check if game over condition is not reached
        {
            // Apply movement and rotation based on player input
            ApplyMovement();
            ApplyRotation();
            HandleMovementInput();
            HandleJumpInput();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) // Check if the player collides with an enemy
        {
            if (playerHealth <= 0)
            {
                // Set the game over flag to true
                isGameOver = true;
                
                // Show the game over panel
                gameOverPanel.SetActive(true);
                // Freeze the time scale to pause the game
                PlayerDied?.Invoke("Wanna Cry");    
            }
            else
            {
                // Decrease player health
                playerHealth--;
                // Update health UI
                UpdateHealthUI();
                // Destroy the enemy
                Destroy(collision.gameObject);
            }
        }
    }

    private void HandleMovementInput()
    {




        // Calculate movement direction based on input
        Vector3 moveDirection = transform.right * moveHorizontal + transform.forward * moveVertical;

        // Calculate the desired position based on movement input
        Vector3 desiredPosition = transform.position + moveDirection.normalized * playerSpeed * Time.deltaTime;

        // Clamp the desired position within the maximum radius
        Vector3 playerToStart = desiredPosition - startPosition;
        if (playerToStart.magnitude <= maxRadius)
        {
            // Apply movement using Rigidbody's velocity
            rb.MovePosition(desiredPosition);
        }
        else
        {
            // If the desired position exceeds the maximum radius, clamp it to the boundary
            Vector3 clampedPosition = startPosition + playerToStart.normalized * maxRadius;
            rb.MovePosition(clampedPosition);
        }
    }

    private void HandleJumpInput()
    {
        // Check for jump input and if the player is grounded
        if (jumpReq)
        {

            // Apply jump force to the Rigidbody's vertical velocity
            rb.velocity += Vector3.up * jumpForce;
            jumpReq = false;
        }
    }

    private void ApplyRotation()
    {
        // Get input for mouse movement
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.fixedDeltaTime;
        // Rotate the player's body horizontally based on mouse input
        playerBody.Rotate(Vector3.up * mouseX);
    }

    private void ApplyMovement()
    {
        // Get input for mouse vertical movement
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.fixedDeltaTime;

        // Update the current rotation angle around the X-axis
        xRotation -= mouseY;

        // Clamp the rotation angle to the specified range
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Apply the clamped rotation to the camera
        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    private void UpdateHealthUI()
    {
        // Hide all hearts
        foreach (GameObject heart in hearts)
        {
            heart.SetActive(false);
        }

        // Show hearts based on player health
        for (int i = 0; i < playerHealth; i++)
        {
            hearts[i].SetActive(true);
        }
    }

    private bool IsGrounded()
    {
        // Cast a sphere downward to check if the player is grounded
        return Physics.SphereCast(transform.position, groundCheckRadius, Vector3.down, out _, 1f, groundLayer);
    }

    public void RestoreHealth(int health)
    {
        if (playerHealth < maxPlayerHealth)
            playerHealth = playerHealth + health;
        UpdateHealthUI();
    }
    public void AddAmmo(int ammount)
    {
        ApexPredatorAssaultRifle[] ammo = GetComponentsInChildren<ApexPredatorAssaultRifle>(true);
        foreach (ApexPredatorAssaultRifle rifle in ammo)
        {
            rifle.AddAmmo(ammount);
        }

    }
}
