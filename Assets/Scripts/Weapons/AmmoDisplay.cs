using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmmoDisplay : MonoBehaviour
{

    public ApexPredatorAssaultRifle assaultRifle; // Reference to the assault rifle script
    public TextMeshProUGUI ammoText; // Reference to the TextMeshPro text component

    void Update()
    {
        // Check if the assaultRifle reference is set and the ammoText reference is set
        if (assaultRifle != null && ammoText != null)
        {
            // Update the ammo text to display the current ammo count
            ammoText.text =   assaultRifle.GetAmmoCount();
        }
    }
}
