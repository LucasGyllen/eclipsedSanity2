using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPlayer : MonoBehaviour, IInteractable
{
    [SerializeField] private float healAmount = 40f;
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
        if (playerHealth != null)
        {
            playerHealth.Heal(healAmount);

            Destroy(gameObject);
        }
    }
}
