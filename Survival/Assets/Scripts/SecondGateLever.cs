using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondGateLever : MonoBehaviour, IInteractable
{
    [SerializeField] private Gate gate;

    public void Interact()
    {
        if (gate != null)
        {
            gate.Interact();
        }
        else
        {
            Debug.LogWarning("Gate reference not set in " + gameObject.name);
        }
    }
}
