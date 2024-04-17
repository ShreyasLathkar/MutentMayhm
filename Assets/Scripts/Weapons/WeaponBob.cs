using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBob : MonoBehaviour
{
    public Transform weaponTransform; // Reference to the weapon's transform
    public float bobbingAmount = 0.05f; // Amount of weapon bobbing
    public float bobbingSpeed = 0.1f; // Base speed of the weapon bobbing
    public float bobbingSmoothing = 3f; // Smoothing factor for weapon bobbing
    public Rigidbody playerRigidbody; // Reference to the player's Rigidbody component

    private float defaultPosY; // Default Y position of the weapon
    private Vector3 previousPosition; // Previous position of the player

    void Start()
    {
        // Store the default Y position of the weapon
        defaultPosY = weaponTransform.localPosition.y;
        // Initialize the previous position of the player
        previousPosition = playerRigidbody.position;
    }

    void Update()
    {
        // Calculate the movement speed based on the change in position of the player's Rigidbody
        float moveSpeed = (playerRigidbody.position - previousPosition).magnitude / Time.deltaTime;
        previousPosition = playerRigidbody.position;

        // Calculate weapon bobbing speed based on player movement speed
        float currentBobbingSpeed = bobbingSpeed + moveSpeed * 0.1f; // Adjust the multiplier as needed

        // Calculate weapon bobbing based on time
        float waveslice = Mathf.Sin(Time.time * currentBobbingSpeed);

        // Calculate weapon bobbing amount
        float translatePosY = waveslice * bobbingAmount;

        // Apply weapon bobbing with smoothing
        Vector3 newPos = weaponTransform.localPosition;
        newPos.y = Mathf.Lerp(newPos.y, defaultPosY + translatePosY, Time.deltaTime * bobbingSmoothing);
        weaponTransform.localPosition = newPos;
    }
}
