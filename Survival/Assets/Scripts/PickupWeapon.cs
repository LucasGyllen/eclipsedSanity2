using UnityEngine;

public class PickupWeapon : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject weaponToPickup;
    [SerializeField] private WeaponSwitcher weaponSwitcher;

    public void Interact()
    {
        if (weaponSwitcher != null && weaponToPickup != null)
        {
            //Add weapon
            weaponSwitcher.weapons.Add(weaponToPickup);

            //Switch to weapon
            weaponSwitcher.SwitchToWeapon(weaponSwitcher.weapons.Count - 1);

            Destroy(gameObject);
        }
    }
}