using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantMaulerGrenadeLauncher : MonoBehaviour
{
    public GameObject grenadePrefab; // Prefab of the grenade
    public Transform firePoint; // Point where grenades are spawned

    void Update()
    {
        // Check if the fire button is pressed
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Instantiate a grenade at the fire point
        GameObject grenadeInstance = Instantiate(grenadePrefab, firePoint.position, firePoint.rotation);

        // Optionally, apply force to the grenade
        Rigidbody grenadeRigidbody = grenadeInstance.GetComponent<Rigidbody>();
        if (grenadeRigidbody != null)
        {
            // Apply force in the forward direction of the firePoint
            grenadeRigidbody.AddForce(firePoint.forward * 30, ForceMode.Impulse);
        }
    }
}
