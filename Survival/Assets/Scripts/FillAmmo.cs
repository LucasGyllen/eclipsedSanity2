using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillAmmo : MonoBehaviour, IInteractable
{
    [SerializeField] private Gun[] guns;

    public void Interact()
    {
        bool shouldDestroy = false; // Flag to determine if the ammo pack should be destroyed
        foreach (Gun gun in guns)
        {
            if (gun != null && gun.CanRefillAmmo())
            {
                Debug.Log("h");
                gun.fillAmmo(); // Call the method to fill ammo
                shouldDestroy = true; // Set flag to true since at least one gun needed ammo
            }
        }

        // If any gun was refilled, destroy the ammo pack
        if (shouldDestroy)
        {
            Destroy(gameObject);
        }
    }
}
