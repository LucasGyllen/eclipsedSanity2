using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPlayer : MonoBehaviour, IInteractable
{
    [SerializeField] private float healAmount = 40f;
    [SerializeField] private GameObject firstAidText = null;
    private GameObject player;
    private PlayerHealth playerHealth;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerHealth = player.GetComponent<PlayerHealth>();
            
        }
    }

    public void Interact()
    {
        if (playerHealth != null && playerHealth.CurrentHealth() != 100)
        {
            playerHealth.Heal(healAmount);

            Destroy(gameObject);
        }
        else if (playerHealth != null)
        {
            firstAidText.SetActive(true);
            StartCoroutine(Delay());
       
        }
    }

    IEnumerator Delay()
    {
        Debug.Log("Delay..");
        yield return new WaitForSeconds(1f);
        Debug.Log("Done!");
        firstAidText.SetActive(false);
    }
}
