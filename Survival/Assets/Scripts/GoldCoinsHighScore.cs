using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoldCoinsHighscore : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI goldCoinsHighscore;

    private void Start()
    {
        goldCoinsHighscore.text = $"You Collected: {GoldCoinsInventory.coinsCounter} Coins!";
    }
}
