using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApexPredatorAssaultRifle : MonoBehaviour
{
    public float damage = 5f; // Damage per bullet
    public float fireRate = 10f; // Shots per second
    public float range = 100f; // Maximum range of the assault rifle
    public Transform firePoint; // Point where bullets are spawned
    public GameObject bulletPrefab; // Prefab of the bullet
    public GameObject muzzleFlash; // Muzzle flash effect
    public int magazineSize = 30; // Maximum number of bullets in a magazine
    public float reloadTime = 2f; // Time it takes to reload in seconds
    public float muzzleFlashRotationSpeed = 500f; // Speed of muzzle flash rotation
    public int maxAmmo = 300; // Maximum ammo capacity

    private int bulletsLeft; // Number of bullets left in the current magazine
    private float nextTimeToFire = 0f; // Time when the weapon can fire next
    private bool isReloading = false; // Flag to indicate if the weapon is currently reloading
    private int currentAmmo; // Current ammo count


    public float reloadMoveDistance = 2f; // Distance to move the gun down during reloading
    public float reloadMoveSpeed =10; // Speed of the gun movement during reloading
    private Vector3 initialPosition; // Initial position of the gun

    private void Start()
    {
        bulletsLeft = magazineSize;
        currentAmmo = maxAmmo;
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && !isReloading && bulletsLeft > 0)
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R) && !isReloading && currentAmmo > 0 && bulletsLeft < magazineSize)
        {
            Reload();
        }
    }

    void Shoot()
    {
        // Spawn a bullet at the fire point if there are bullets left in the magazine
        if (bulletsLeft > 0)
        {
            // Decrease the number of bullets left
            bulletsLeft--;

            // Update the time when the weapon can fire next
            nextTimeToFire = Time.time + 1f / fireRate;

            // Instantiate bullet
            GameObject bulletInstance = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Bullet projectile = bulletInstance.GetComponent<Bullet>();
            if (projectile != null)
            {
                projectile.SetDamage(damage);
                projectile.SetRange(range);
            }

            // Activate muzzle flash
            muzzleFlash.SetActive(true);
            // Rotate muzzle flash
            RotateMuzzleFlash();

            // Deactivate muzzle flash after a short duration
            Invoke("DeactivateMuzzleFlash", 0.1f);

            // Check if the magazine is empty after shooting
            if (bulletsLeft == 0)
            {
                // Start reloading if the magazine is empty
                Reload();
            }
        }
    }

    void RotateMuzzleFlash()
    {
        // Rotate muzzle flash
        muzzleFlash.transform.Rotate(Vector3.right, muzzleFlashRotationSpeed * Time.deltaTime);
    }

    void Reload()
    {
        // Start reloading
        isReloading = true;

        StartCoroutine(MoveGunDown());
        // Play reload animation or sound effect

        // Set a timer to finish reloading
        Invoke("FinishReloading", reloadTime);
    }

    IEnumerator MoveGunDown()
    {
        float elapsedTime = 0f;
        Vector3 targetPosition = initialPosition - new Vector3(0f, reloadMoveDistance, 0f);

        while (elapsedTime < reloadTime/2)
        {
            transform.localPosition = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / (reloadTime/2));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    void FinishReloading()
    {
        
        // Refill the magazine to full capacity or the remaining ammo if less than magazine size
        bulletsLeft = Mathf.Min(magazineSize, currentAmmo);

        // Subtract the used ammo from current ammo
        currentAmmo -= bulletsLeft;


        StartCoroutine(MoveGunUp());
        // Finish reloading
        isReloading = false;
    }

    IEnumerator MoveGunUp()
    {
        float elapsedTime = 0f;
        Vector3 targetPosition = initialPosition;

        while (elapsedTime < reloadMoveSpeed)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, elapsedTime / reloadMoveSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = targetPosition;
    }

    void DeactivateMuzzleFlash()
    {
        // Deactivate muzzle flash effect
        muzzleFlash.SetActive(false);
    }
    public string GetAmmoCount()
    {
        string bulletCount =bulletsLeft.ToString() + "/"+ currentAmmo.ToString();
        return bulletCount;
    }
    public void AddAmmo(int ammo)
    {
        currentAmmo += ammo;
    }
}
