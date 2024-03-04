using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    public List<GameObject> weapons = new List<GameObject>();

    private int currentWeaponIndex = 0;

    public int getCurrentWeaponIndex () { return currentWeaponIndex; }
    public List<GameObject> getWeapons() { return weapons; }

    void Start()
    {
        UpdateWeaponActiveState();
    }

    void Update()
    {
        int previousWeaponIndex = currentWeaponIndex;

        // Switch weapon with scroll wheel
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (currentWeaponIndex >= weapons.Count - 1)
                currentWeaponIndex = 0;
            else
                currentWeaponIndex++;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (currentWeaponIndex <= 0)
                currentWeaponIndex = weapons.Count - 1;
            else
                currentWeaponIndex--;
        }

        // Switch weapon with number keys
        if (Input.GetKeyDown(KeyCode.Alpha1)) currentWeaponIndex = 0;
        if (Input.GetKeyDown(KeyCode.Alpha2) && weapons.Count >= 2) currentWeaponIndex = 1;
        if (Input.GetKeyDown(KeyCode.Alpha3) && weapons.Count >= 3) currentWeaponIndex = 2;
        if (Input.GetKeyDown(KeyCode.Alpha4) && weapons.Count >= 4) currentWeaponIndex = 3;
        if (Input.GetKeyDown(KeyCode.Alpha5) && weapons.Count >= 5) currentWeaponIndex = 4;


        // Update the active weapon if it has changed
        if (previousWeaponIndex != currentWeaponIndex)
        {
            UpdateWeaponActiveState();
        }
    }

    public void ShootActiveWeapon()
    {
        if (currentWeaponIndex >= 0 && currentWeaponIndex < weapons.Count)
        {
            Gun gunComponent = weapons[currentWeaponIndex].GetComponent<Gun>();
            if (gunComponent != null)
            {
                gunComponent.Shoot();
            }
        }
    }

    public void ReloadActiveWeapon()
    {
        if (currentWeaponIndex >= 0 && currentWeaponIndex < weapons.Count)
        {
            Gun gunComponent = weapons[currentWeaponIndex].GetComponent<Gun>();
            if (gunComponent != null)
            {
                gunComponent.StartReload();
            }
        }
    }

    void UpdateWeaponActiveState()
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            // Enable the current weapon and disable all others
            weapons[i].SetActive(i == currentWeaponIndex);
        }
    }

    public void SwitchToWeapon(int index)
    {
        if (index >= 0 && index < weapons.Count)
        {
            currentWeaponIndex = index;
            UpdateWeaponActiveState();
        }
    }
}
