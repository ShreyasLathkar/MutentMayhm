using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScavengersShotgun : MonoBehaviour
{
    public float damage = 10f; // Damage per pellet
    public float fireRate = 1f; // Shots per second
    public int pellets = 8; // Number of pellets per shot
    public float spreadAngle = 20f; // Spread angle of the pellets
    public float range = 10f; // Maximum range of the shotgun
    public Transform firePoint; // Point where bullets are spawned
    public GameObject bulletPrefab; // Prefab of the bullet
    public GameObject muzzleFlash; // Muzzle flash effect
    public int magazineSize = 8; // Maximum number of bullets in a magazine
    public float reloadTime = 2f; // Time it takes to reload in seconds

    private int bulletsLeft; // Number of bullets left in the current magazine
    private float nextTimeToFire = 0f; // Time when the weapon can fire next
    private bool isReloading = false; // Flag to indicate if the weapon is currently reloading

    private void Start()
    {
        bulletsLeft = magazineSize;
    }

    void Update()
    {
        // Check if the fire button is pressed, the weapon is ready to fire, and not currently reloading
        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire && !isReloading)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }

        // Check if the reload button is pressed and the weapon is not currently reloading
        if (Input.GetKeyDown(KeyCode.R) && !isReloading)
        {
            Reload();
        }
    }

    void Shoot()
    {
        // Check if there are bullets left in the magazine
        if (bulletsLeft > 0)
        {
            // Activate muzzle flash
            muzzleFlash.SetActive(true);
            // Deactivate muzzle flash after a short duration
            Invoke("DeactivateMuzzleFlash", 0.1f);

            // Loop through the number of pellets and spawn bullets with random spread
            for (int i = 0; i < pellets; i++)
            {
                Vector3 spread = Random.insideUnitCircle * spreadAngle;
                Quaternion spreadRotation = Quaternion.Euler(spread.x, spread.y, 0f);
                GameObject bulletInstance = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation * spreadRotation);
                Bullet projectile = bulletInstance.GetComponent<Bullet>();
                if (projectile != null)
                {
                    projectile.SetDamage(damage);
                    projectile.SetRange(range);
                }
            }

            // Decrease the number of bullets left
            bulletsLeft--;

            // Check if the magazine is empty after shooting
            if (bulletsLeft == 0)
            {
                // Start reloading if the magazine is empty
                Reload();
            }
        }
    }

    void Reload()
    {
        // Start reloading
        isReloading = true;

        // Play reload animation or sound effect

        // Set a timer to finish reloading
        Invoke("FinishReloading", reloadTime);
    }

    void FinishReloading()
    {
        // Refill the magazine to full capacity
        bulletsLeft = magazineSize;

        // Finish reloading
        isReloading = false;
    }

    void DeactivateMuzzleFlash()
    {
        // Deactivate muzzle flash effect
        muzzleFlash.SetActive(false);
    }
}
