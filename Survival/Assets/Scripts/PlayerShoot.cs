using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public static Action shootInput;
    public static Action reloadInput;

    [SerializeField] private KeyCode reloadKey;
    [SerializeField] private WeaponSwitcher weaponSwitcher;

    //public AudioClip GunFire;
    //private AudioSource audioSource;

    private void start()
    {
        //audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            weaponSwitcher.ShootActiveWeapon();

            //PlaySound(GunFire);
        }

        if (Input.GetKeyDown(reloadKey))
        {
            weaponSwitcher.ReloadActiveWeapon();
        }
    }

    /*private void PlaySound(AudioClip soundClip)
    {
        if (!audioSource.isPlaying || audioSource.clip != soundClip)
        {
            audioSource.clip = soundClip;
            audioSource.Play();
        }
    }*/
}
