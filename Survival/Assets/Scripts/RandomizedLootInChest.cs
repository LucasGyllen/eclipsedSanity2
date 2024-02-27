using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizedLootInChest : MonoBehaviour
{
    public GameObject[] drops;

    public int randomNumber;

    private void Start()
    {
        randomNumber = Random.Range(0, drops.Length);

        if (drops[randomNumber] != null)
            drops[randomNumber].SetActive(true);
    }
}
