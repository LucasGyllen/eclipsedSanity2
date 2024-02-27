using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactToOpen : MonoBehaviour, IInteractable
{
    private Animator animator;
    [SerializeField] private string interactAnimation;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Interact()
    {
        animator.Play(interactAnimation, 0, 0.0f);
    }
}
