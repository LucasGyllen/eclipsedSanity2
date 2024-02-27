using UnityEngine;
using UnityEngine.UI;

public class ObjectLookedAtByPlayer : MonoBehaviour, iGazeReceiver
{
    private bool isGazingUpon;

    [SerializeField] private GameObject UIText = null;
    [SerializeField] private int timeToShowUI = 1;

    void Update()
    {
        if (isGazingUpon)
        {
            UIText.SetActive(true);
        }
        else
        {
            UIText.SetActive(false);
        }
    }

    public void GazingUpon()
    {
        isGazingUpon = true;
    }

    public void NotGazingUpon()
    {
        isGazingUpon = false;
    }
}
