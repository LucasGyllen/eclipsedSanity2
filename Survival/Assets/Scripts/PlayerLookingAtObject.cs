using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface iGazeReceiver
{
    // Should be called when the object is being looked at
    void GazingUpon();

    // Should be called when the object is no longer being looked at
    void NotGazingUpon();
}

public class PlayerLookingAtObject : MonoBehaviour
{
    public Camera viewCamera;
    private GameObject lastLookedAt;

    void Update()
    {
        CheckIfLooking();
    }

    private void CheckIfLooking()
    {
        if (lastLookedAt)
        {
            lastLookedAt.SendMessage("NotGazingUpon", SendMessageOptions.DontRequireReceiver);
        }

        Ray lookingRay = new Ray(viewCamera.transform.position, viewCamera.transform.rotation * Vector3.forward);
        RaycastHit hit;

        if (Physics.Raycast(lookingRay, out hit, Mathf.Infinity))
        {
            hit.transform.SendMessage("GazingUpon", SendMessageOptions.DontRequireReceiver);
            lastLookedAt = hit.transform.gameObject;
        }
    }
}
