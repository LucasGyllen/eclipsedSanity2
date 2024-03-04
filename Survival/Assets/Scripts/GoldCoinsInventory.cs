using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GoldCoinsInventory : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinsCollected;
    public static int coinsCounter;
    // Start is called before the first frame update
    void Start()
    {
        coinsCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        coinsCollected.text = $"Coins: {coinsCounter}";
    }

    public void AddCoins(int amount)
    {
        coinsCounter += amount;
    }
}
