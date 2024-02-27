using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    private GameObject OB;

    [SerializeField] private Animator chestAnim;

    void Start()
    {
        OB = this.gameObject;
    }

    public void Interact()
    {
        OB.GetComponent<Animator>().SetBool("open", true);
        OB.GetComponent<BoxCollider>().enabled = false;
    }
}
