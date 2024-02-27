using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCoins : MonoBehaviour, IInteractable
{
    [SerializeField] private int coinsAmount = 320;
    private GameObject player;
    private GoldCoinsInventory coinsInventory;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            coinsInventory = player.GetComponent<GoldCoinsInventory>();
        }
    }

    public void Interact()
    {
        if (coinsInventory != null)
        {
            coinsInventory.AddCoins(coinsAmount);

            Destroy(gameObject);
        }
    }
}
