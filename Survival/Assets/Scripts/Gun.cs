using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    [Header("References")]
    [SerializeField] gunData gunData;
    [SerializeField] private Transform muzzleEffect;
    [SerializeField] private TextMeshProUGUI shotsRemaining;

    public ImpactInfo[] ImpactElemets = new ImpactInfo[0];
    [Space]
    public GameObject ImpactEffect;

    public AudioClip GunFire;
    private AudioSource audioSource;
    private Animator animator;

    bool isCrouching = false;
    float timeSinceLastShot;

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        audioSource = player.GetComponent<AudioSource>();

        animator = GetComponent<Animator>();

        gunData.currentAmmo = gunData.magSize;
        gunData.currentMagAmount = gunData.maxMagAmount - 2;

        gunData.reloading = false; //Makes sure the gunData reloading bool doesn't start as true
    }

    public void StartReload()
    {
        if (!gunData.reloading && gunData.currentAmmo != gunData.magSize && gunData.currentMagAmount > 0)
        {
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload()
    {
        gunData.reloading = true;
        animator.SetBool("IsReloading", true);
        yield return new WaitForSeconds(gunData.reloadTime);
        gunData.currentAmmo = gunData.magSize;
        gunData.currentMagAmount--;
        animator.SetBool("IsReloading", false);
        gunData.reloading = false;
    }

    private bool CanShoot() => !gunData.reloading && timeSinceLastShot > 1f / (gunData.fireRate / 60f);

    public void Shoot()
    {
        if (gunData.currentAmmo > 0 && gunData.currentMagAmount >= 0)
        {
            if (CanShoot())
            {
                PlaySound(GunFire);
                animator.Play("Shoot");

                if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hitInfo, gunData.maxDistance))
                {
                    IDamageable damageable = hitInfo.transform.GetComponent<IDamageable>();
                    damageable?.Damage(gunData.damage);

                    /*The ImpactEffect was somehow running the shoot function multiple times when shooting an enemy, 
                        resulting in the player dealing massive damage to enemies almost one shooting them,
                        this removes the ImpactEffect for objects with Enemy tags.*/
                    if (!hitInfo.collider.CompareTag("Enemy"))
                    {
                        var effect = GetImpactEffect(hitInfo.transform.gameObject);
                        if (effect == null)
                            return;
                        var effectIstance = Instantiate(effect, hitInfo.point, new Quaternion()) as GameObject;
                        effectIstance.transform.LookAt(hitInfo.point + hitInfo.normal);
                        Destroy(effectIstance, 20);
                    }
                    var impactEffectIstance = Instantiate(ImpactEffect, muzzleEffect.position, muzzleEffect.rotation) as GameObject;

                    Destroy(impactEffectIstance, 4); 
                }

                gunData.currentAmmo--;
                timeSinceLastShot = 0;

                Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * gunData.maxDistance, Color.red, 1.0f);
            }
        }
        else
        {
            //Debug.Log("Out of ammo!");
        }
    }

    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;

        shotsRemaining.text = $"Ammo: {gunData.currentAmmo} / {gunData.magSize}\n Mags: {gunData.currentMagAmount} / {gunData.maxMagAmount}";

        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * gunData.maxDistance, Color.green, 0.1f);
    }

    [System.Serializable]
    public class ImpactInfo
    {
        public MaterialType.MaterialTypeEnum MaterialType;
        public GameObject ImpactEffect;
    }

    GameObject GetImpactEffect(GameObject impactedGameObject)
    {
        var materialType = impactedGameObject.GetComponent<MaterialType>();

        if (materialType == null)
            return null;

        foreach (var impactInfo in ImpactElemets)
        {
            if (impactInfo.MaterialType == materialType.TypeOfMaterial)
                return impactInfo.ImpactEffect;
        }
        return null;
    }

    private void PlaySound(AudioClip soundClip)
    {
        audioSource.Stop();

        audioSource.clip = soundClip;
        audioSource.Play(); 
    }

    public bool CanRefillAmmo()
    {
        return gunData.currentMagAmount < gunData.maxMagAmount;
    }

    public void fillAmmo()
    {
        gunData.currentMagAmount++;
    }
}
