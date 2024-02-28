using UnityEngine;
using UnityEngine.UI;

public class ObjectLookedAtByPlayer : MonoBehaviour, iGazeReceiver
{
    private bool isGazingUpon;

    [SerializeField] private GameObject UIText = null;
    [SerializeField] private int timeToShowUI = 1;
    [SerializeField] private bool textStay = false;

    void Update()
    {
        if (isGazingUpon)
        {
            UIText.SetActive(true);
        }
        else if (!textStay)
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
