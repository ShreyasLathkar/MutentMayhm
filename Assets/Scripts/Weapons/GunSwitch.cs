using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSwitch : MonoBehaviour
{
    public GameObject[] guns; // Array of gun GameObjects to switch between
    private int currentGunIndex = 0; // Index of the currently active gun

    void Start()
    {
        // Ensure only the first gun is active at the start
        SwitchGun(currentGunIndex);
    }

    void Update()
    {
        // Check if the player scrolled the mouse wheel
        float scrollDelta = Input.GetAxis("Mouse ScrollWheel");
        if (scrollDelta != 0)
        {
            // Calculate the new gun index based on the scroll direction
            int newIndex = currentGunIndex + (scrollDelta > 0 ? 1 : -1);
            // Ensure the new index is within the bounds of the guns array
            newIndex = Mathf.Clamp(newIndex, 0, guns.Length - 1);
            // Switch to the new gun
            SwitchGun(newIndex);
        }
    }

    void SwitchGun(int newIndex)
    {
        // Deactivate the current gun
        guns[currentGunIndex].SetActive(false);

        // Update the current gun index
        currentGunIndex = newIndex;

        // Activate the new gun
        guns[currentGunIndex].SetActive(true);
    }
}
