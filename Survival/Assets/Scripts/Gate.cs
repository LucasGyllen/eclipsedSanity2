using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour, IInteractable
{
    private Animator animator;
    [SerializeField] private string open;
    [SerializeField] private string close;
    [SerializeField] private float interactionCooldown = 4.45f;

    private bool isOpen = false;
    private bool isInteractable = true;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Interact()
    {
        if (isInteractable)
        {
            if (isOpen)
            {
                animator.Play(close, 0, 0.0f);
            }
            else
            {
                animator.Play(open, 0, 0.0f);
            }
            isOpen = !isOpen;
            StartCoroutine(PauseInteraction());
        }
    }

    private IEnumerator PauseInteraction()
    {
        isInteractable = false;
        yield return new WaitForSeconds(interactionCooldown);
        isInteractable = true;
    }
}
