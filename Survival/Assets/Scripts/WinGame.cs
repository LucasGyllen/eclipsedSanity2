using UnityEngine;
using UnityEngine.SceneManagement;

public class WinGame : MonoBehaviour
{
    public string winSceneName;
    public float delay = 0.5f;
    public GameObject fadeout;

    private bool playerInsideTrigger = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInsideTrigger = true;
            fadeout.SetActive(true);
            Invoke("LoadWinScene", delay);
        }
    }

    private void LoadWinScene()
    {
        if (playerInsideTrigger)
        {
            SceneManager.LoadScene(winSceneName);
        }
    }
}
