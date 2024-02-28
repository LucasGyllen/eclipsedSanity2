using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    Transform mainCam;
    Transform unit;
    Transform worldSpaceCanvas;

    [SerializeField] private Canvas canvas = null;
    [SerializeField] private GameObject item = null;

    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main.transform;
        unit = transform.parent;
        worldSpaceCanvas = canvas.transform;

        transform.SetParent(worldSpaceCanvas);
    }

    // Update is called once per frame
    void Update()
    {
        if (item != null)
        {
             transform.rotation = Quaternion.LookRotation(transform.position - mainCam.transform.position);
             transform.position = unit.position + offset;
        }
        else
        {
            gameObject.SetActive(false);
        }
           
    }
}
